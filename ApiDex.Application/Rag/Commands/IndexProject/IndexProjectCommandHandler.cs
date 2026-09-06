using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.Rag;
using MediatR;

namespace ApiDex.Application.Rag.Commands.IndexProject;

public class IndexProjectCommandHandler(
    IDocumentationProjectRepository projectRepository,
    IEmbeddingService embeddingService,
    IVectorStoreRepository vectorStoreRepository,
    IGraphStoreRepository graphStoreRepository) : IRequestHandler<IndexProjectCommand, bool>
{
    public async Task<bool> Handle(IndexProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetProjectDataForIndexingAsync(request.ProjectId, cancellationToken);
        if (project is null) return false;

        var chunks = DocumentationChunkFactory.CreateAll(project);

        var textsToEmbed = chunks.Select(c => c.Content).ToList();
        var embeddings = await embeddingService.EmbedBatchAsync(textsToEmbed, cancellationToken);

        if (embeddings.Count != chunks.Count)
        {
            throw new InvalidOperationException(
                $"Expected {chunks.Count} embeddings, but received {embeddings.Count}.");
        }

        var embeddedChunks = chunks.Select((chunk, index) => new EmbeddedChunk
        {
            Id = chunk.Id,
            Content = chunk.Content,
            Embedding = embeddings[index],
            ProjectId = chunk.ProjectId,
            ServiceId = chunk.ServiceId,
            EndpointId = chunk.EndpointId,
            Source = chunk.Source
        }).ToList();

        await vectorStoreRepository.DeleteByProjectIdAsync(project.Id, cancellationToken: cancellationToken);
        await vectorStoreRepository.UpsertAsync(embeddedChunks, cancellationToken);
        await graphStoreRepository.SyncProjectAsync(project, cancellationToken);

        return true;
    }
}
