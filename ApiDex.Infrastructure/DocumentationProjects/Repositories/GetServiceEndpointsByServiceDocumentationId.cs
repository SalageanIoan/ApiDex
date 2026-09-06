using ApiDex.Domain.DocumentationProjects;
using ApiDex.Infrastructure.DocumentationProjects.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<List<ServiceEndpoint>> GetServiceEndpointsByServiceDocumentationIdAsync(
        Guid serviceDocumentationId, CancellationToken cancellationToken = default)
    {
        var endpoints = await context.ServiceEndpoints
            .AsNoTracking()
            .Include(entity => entity.ServiceDocumentation)
            .Where(entity => entity.ServiceDocumentationId == serviceDocumentationId)
            .ToListAsync(cancellationToken);

        return endpoints.Select(DocumentationProjectMappings.MapToServiceEndpoint).ToList();
    }
}