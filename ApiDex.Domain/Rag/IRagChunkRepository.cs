namespace ApiDex.Domain.Rag;

public interface IRagChunkRepository
{
    Task<List<RetrievedChunk>> HydrateAsync(
        IReadOnlyList<RetrievedChunk> rankedChunks,
        CancellationToken cancellationToken = default);
}
