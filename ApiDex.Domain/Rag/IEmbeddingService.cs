namespace ApiDex.Domain.Rag;

public interface IEmbeddingService
{
    Task<float[]> EmbedAsync(string text, CancellationToken cancellationToken = default);

    Task<List<float[]>> EmbedBatchAsync(IReadOnlyList<string> texts, CancellationToken cancellationToken = default);
}