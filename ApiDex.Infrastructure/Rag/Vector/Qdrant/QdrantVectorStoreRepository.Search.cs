using ApiDex.Domain.Rag;
using Qdrant.Client.Grpc;

namespace ApiDex.Infrastructure.Rag.Vector.Qdrant;

public partial class QdrantVectorStoreRepository
{
    public async Task<List<RetrievedChunk>> SearchAsync(float[] queryEmbedding, int topK, Guid? projectId = null,
        Guid? serviceId = null, CancellationToken cancellationToken = default)
    {
        var activeModel = await settingsManager.GetActiveModelAsync(cancellationToken);
        await EnsureCollectionAsync(activeModel, cancellationToken);

        var points = await _client.SearchAsync(
            CollectionName(activeModel),
            queryEmbedding,
            filter: CreateFilter(projectId, serviceId),
            limit: (ulong)topK,
            payloadSelector: true,
            cancellationToken: cancellationToken);

        return points
            .Select(ToRetrievedChunk)
            .Where(chunk => chunk is not null)
            .Select(chunk => chunk!)
            .ToList();
    }
}
