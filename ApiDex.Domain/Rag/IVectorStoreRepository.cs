namespace ApiDex.Domain.Rag;

public interface IVectorStoreRepository
{
    Task EnsureCollectionAsync(CancellationToken cancellationToken = default);

    Task UpsertAsync(IReadOnlyList<EmbeddedChunk> chunks, CancellationToken cancellationToken = default);

    Task<List<RetrievedChunk>> SearchAsync(float[] queryEmbedding, int topK,
        Guid? projectId = null, Guid? serviceId = null,
        CancellationToken cancellationToken = default);

    Task DeleteByProjectIdAsync(Guid projectId, bool allModels = false, CancellationToken cancellationToken = default);

    Task<int> CountByProjectIdAsync(Guid projectId, bool allModels = false, CancellationToken cancellationToken = default);
}
