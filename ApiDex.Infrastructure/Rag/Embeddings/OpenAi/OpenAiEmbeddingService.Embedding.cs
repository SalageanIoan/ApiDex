namespace ApiDex.Infrastructure.Rag.Embeddings.OpenAi;

public partial class OpenAiEmbeddingService
{
    public async Task<float[]> EmbedAsync(string text, CancellationToken cancellationToken = default)
    {
        var result = await EmbedBatchAsync([text], cancellationToken);
        return result[0];
    }

    public async Task<List<float[]>> EmbedBatchAsync(IReadOnlyList<string> texts, CancellationToken cancellationToken = default)
    {
        if (texts.Count == 0) return [];

        var response = await EmbeddingClient.GenerateEmbeddingsAsync(
            texts,
            cancellationToken: cancellationToken);
        var embeddings = response.Value
            .Select(embedding => embedding.ToFloats().ToArray())
            .ToList();

        if (embeddings.Count != texts.Count)
        {
            throw new InvalidOperationException(
                $"Expected {texts.Count} embeddings, but OpenAI returned {embeddings.Count}.");
        }

        return embeddings;
    }
}
