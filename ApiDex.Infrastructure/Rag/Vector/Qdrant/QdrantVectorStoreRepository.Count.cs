namespace ApiDex.Infrastructure.Rag.Vector.Qdrant;

public partial class QdrantVectorStoreRepository
{
    public async Task<int> CountByProjectIdAsync(Guid projectId, bool allModels = false,
        CancellationToken cancellationToken = default)
    {
        var models = allModels ? AllModels() : [await settingsManager.GetActiveModelAsync(cancellationToken)];
        var count = 0;
        foreach (var model in models)
        {
            await EnsureCollectionAsync(model, cancellationToken);
            count += (int)await _client.CountAsync(
                CollectionName(model),
                CreateFilter(projectId, null),
                exact: true,
                cancellationToken: cancellationToken);
        }

        return count;
    }
}
