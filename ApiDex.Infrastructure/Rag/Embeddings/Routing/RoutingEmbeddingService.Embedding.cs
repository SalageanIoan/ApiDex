namespace ApiDex.Infrastructure.Rag.Embeddings.Routing;

public partial class RoutingEmbeddingService
{
    public async Task<float[]> EmbedAsync(string text, CancellationToken cancellationToken = default)
    {
        var activeService = await GetActiveServiceAsync(cancellationToken);
        return await activeService.EmbedAsync(text, cancellationToken);
    }

    public async Task<List<float[]>> EmbedBatchAsync(IReadOnlyList<string> texts, CancellationToken cancellationToken = default)
    {
        var activeService = await GetActiveServiceAsync(cancellationToken);
        return await activeService.EmbedBatchAsync(texts, cancellationToken);
    }
}
