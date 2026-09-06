using ApiDex.Domain.Rag;
using Microsoft.Extensions.Logging;
using SmartComponents.LocalEmbeddings;

namespace ApiDex.Infrastructure.Rag.Embeddings.LocalBgeBase;

public class LocalBgeBaseEmbeddingService(ILogger<LocalBgeBaseEmbeddingService> logger) : IEmbeddingService
{
    private readonly LocalEmbedder _embedder = new("bge-base-en-v1.5");

    public Task<float[]> EmbedAsync(string text, CancellationToken cancellationToken = default)
    {
        var embedding = _embedder.Embed(text);
        return Task.FromResult(embedding.Values.ToArray());
    }

    public Task<List<float[]>> EmbedBatchAsync(IReadOnlyList<string> texts, CancellationToken cancellationToken = default)
    {
        var results = new List<float[]>();
        foreach (var text in texts)
        {
            var embedding = _embedder.Embed(text);
            results.Add(embedding.Values.ToArray());
        }

        logger.LogDebug("Generated {Count} embeddings using bge-base-en-v1.5 local model", results.Count);
        return Task.FromResult(results);
    }
}
