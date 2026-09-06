using ApiDex.Domain.DocumentationProjects;

namespace ApiDex.Domain.Rag;

public interface IGraphStoreRepository
{
    Task EnsureSchemaAsync(CancellationToken cancellationToken = default);

    Task SyncProjectAsync(DocumentationProject project, CancellationToken cancellationToken = default);

    Task DeleteProjectAsync(Guid projectId, CancellationToken cancellationToken = default);

    Task<string> GetContextForChunksAsync(IReadOnlyList<RetrievedChunk> chunks, Guid? projectId = null,
        Guid? serviceId = null, RagContextMode contextMode = RagContextMode.Compact,
        CancellationToken cancellationToken = default);
}