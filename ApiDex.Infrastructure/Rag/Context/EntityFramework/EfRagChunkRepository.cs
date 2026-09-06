using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.Rag;
using ApiDex.Infrastructure.Data;
using ApiDex.Infrastructure.DocumentationProjects.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.Rag.Context.EntityFramework;

public sealed class EfRagChunkRepository(ApiDexDbContext context) : IRagChunkRepository
{
    public async Task<List<RetrievedChunk>> HydrateAsync(
        IReadOnlyList<RetrievedChunk> rankedChunks,
        CancellationToken cancellationToken = default)
    {
        if (rankedChunks.Count == 0) return [];

        var chunksById = new Dictionary<string, DocumentationChunk>(StringComparer.Ordinal);

        await LoadProjectChunksAsync(rankedChunks, chunksById, cancellationToken);
        await LoadServiceChunksAsync(rankedChunks, chunksById, cancellationToken);
        await LoadEndpointChunksAsync(rankedChunks, chunksById, cancellationToken);

        return rankedChunks
            .Where(match => chunksById.ContainsKey(match.Id))
            .Select(match => ToRetrievedChunk(chunksById[match.Id], match.Score))
            .ToList();
    }

    private async Task LoadProjectChunksAsync(
        IReadOnlyList<RetrievedChunk> rankedChunks,
        IDictionary<string, DocumentationChunk> chunksById,
        CancellationToken cancellationToken)
    {
        var projectIds = rankedChunks
            .Where(chunk => chunk.ServiceId is null && chunk.EndpointId is null)
            .Select(chunk => chunk.ProjectId)
            .Distinct()
            .ToList();

        if (projectIds.Count == 0) return;

        var projects = await context.DocumentationProjects
            .AsNoTracking()
            .Where(project => projectIds.Contains(project.Id))
            .ToListAsync(cancellationToken);

        foreach (var entity in projects)
        {
            var project = new DocumentationProject
            {
                Id = entity.Id,
                Title = entity.Title,
                GeneralDescription = entity.GeneralDescription,
                ArchitectureType = entity.ArchitectureType,
                Services = []
            };

            var chunk = DocumentationChunkFactory.CreateProject(project);
            chunksById[chunk.Id] = chunk;
        }
    }

    private async Task LoadServiceChunksAsync(
        IReadOnlyList<RetrievedChunk> rankedChunks,
        IDictionary<string, DocumentationChunk> chunksById,
        CancellationToken cancellationToken)
    {
        var serviceIds = rankedChunks
            .Where(chunk => chunk.ServiceId is not null && chunk.EndpointId is null)
            .Select(chunk => chunk.ServiceId!.Value)
            .Distinct()
            .ToList();

        if (serviceIds.Count == 0) return;

        var services = await context.ServiceDocumentations
            .AsNoTracking()
            .AsSplitQuery()
            .Include(service => service.DocumentationProject)
            .Include(service => service.Endpoints)
            .Include(service => service.Events)
                .ThenInclude(serviceEvent => serviceEvent.EventEndpoints)
            .Include(service => service.Dependencies)
            .Where(service => serviceIds.Contains(service.Id))
            .ToListAsync(cancellationToken);

        foreach (var entity in services)
        {
            var service = DocumentationProjectMappings.MapToServiceDocumentation(entity);
            var chunk = DocumentationChunkFactory.CreateService(
                entity.DocumentationProjectId,
                entity.DocumentationProject.Title,
                service);

            chunksById[chunk.Id] = chunk;
        }
    }

    private async Task LoadEndpointChunksAsync(
        IReadOnlyList<RetrievedChunk> rankedChunks,
        IDictionary<string, DocumentationChunk> chunksById,
        CancellationToken cancellationToken)
    {
        var endpointIds = rankedChunks
            .Where(chunk => chunk.EndpointId is not null)
            .Select(chunk => chunk.EndpointId!.Value)
            .Distinct()
            .ToList();

        if (endpointIds.Count == 0) return;

        var endpoints = await context.ServiceEndpoints
            .AsNoTracking()
            .AsSplitQuery()
            .Include(endpoint => endpoint.ServiceDocumentation)
                .ThenInclude(service => service.DocumentationProject)
            .Include(endpoint => endpoint.ServiceDocumentation)
                .ThenInclude(service => service.Events)
                .ThenInclude(serviceEvent => serviceEvent.EventEndpoints)
            .Include(endpoint => endpoint.ServiceDocumentation)
                .ThenInclude(service => service.Dependencies)
            .Where(endpoint => endpointIds.Contains(endpoint.Id))
            .ToListAsync(cancellationToken);

        foreach (var entity in endpoints)
        {
            var endpoint = DocumentationProjectMappings.MapToServiceEndpoint(entity);
            var serviceEntity = entity.ServiceDocumentation;
            var service = new ServiceDocumentation
            {
                Id = serviceEntity.Id,
                Title = serviceEntity.Title,
                GeneralDescription = serviceEntity.GeneralDescription,
                Endpoints = [endpoint],
                Events = serviceEntity.Events
                    .Select(DocumentationProjectMappings.MapToServiceEvent)
                    .ToList(),
                Dependencies = serviceEntity.Dependencies
                    .Select(DocumentationProjectMappings.MapToServiceDependency)
                    .ToList()
            };

            var chunk = DocumentationChunkFactory.CreateEndpoint(
                serviceEntity.DocumentationProjectId,
                serviceEntity.DocumentationProject.Title,
                service,
                endpoint);

            chunksById[chunk.Id] = chunk;
        }
    }

    private static RetrievedChunk ToRetrievedChunk(DocumentationChunk chunk, double score)
    {
        return new RetrievedChunk
        {
            Id = chunk.Id,
            Content = chunk.Content,
            Score = score,
            ProjectId = chunk.ProjectId,
            ServiceId = chunk.ServiceId,
            EndpointId = chunk.EndpointId,
            Source = chunk.Source
        };
    }
}
