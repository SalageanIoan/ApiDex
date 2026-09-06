using ApiDex.Domain.Rag;
using Qdrant.Client.Grpc;

namespace ApiDex.Infrastructure.Rag.Vector.Qdrant;

public partial class QdrantVectorStoreRepository
{
    public async Task UpsertAsync(IReadOnlyList<EmbeddedChunk> chunks, CancellationToken cancellationToken = default)
    {
        if (chunks.Count == 0) return;

        var activeModel = await settingsManager.GetActiveModelAsync(cancellationToken);
        await EnsureCollectionAsync(activeModel, cancellationToken);

        var points = chunks.Select(chunk => new PointStruct
        {
            Id = ToPointId(chunk.Id),
            Vectors = chunk.Embedding,
            Payload = { CreatePayload(chunk, activeModel) }
        }).ToList();

        await _client.UpsertAsync(CollectionName(activeModel), points, wait: true, cancellationToken: cancellationToken);
    }
}
