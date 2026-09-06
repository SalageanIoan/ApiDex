using ApiDex.Domain.Rag;
using ApiDex.Infrastructure.Rag.Settings;
using Qdrant.Client.Grpc;

namespace ApiDex.Infrastructure.Rag.Vector.Qdrant;

public partial class QdrantVectorStoreRepository
{
    public async Task EnsureCollectionAsync(CancellationToken cancellationToken = default)
    {
        var activeModel = await settingsManager.GetActiveModelAsync(cancellationToken);
        await EnsureCollectionAsync(activeModel, cancellationToken);
    }

    private async Task EnsureCollectionAsync(EmbeddingModelDescriptor model, CancellationToken cancellationToken)
    {
        var collectionName = CollectionName(model);
        if (await _client.CollectionExistsAsync(collectionName, cancellationToken))
        {
            return;
        }

        await _client.CreateCollectionAsync(
            collectionName,
            new VectorParams
            {
                Size = (ulong)model.Dimensions,
                Distance = Distance.Cosine
            },
            cancellationToken: cancellationToken);
    }

    private string CollectionName(EmbeddingModelDescriptor model)
    {
        return $"{_collectionPrefix}_{model.Key.Replace("-", "_")}";
    }

    private static IReadOnlyList<EmbeddingModelDescriptor> AllModels()
    {
        return
        [
            RagSettingsManager.ToDescriptor(EmbeddingModelType.OpenAi),
            RagSettingsManager.ToDescriptor(EmbeddingModelType.Local),
            RagSettingsManager.ToDescriptor(EmbeddingModelType.LocalBgeBase)
        ];
    }
}
