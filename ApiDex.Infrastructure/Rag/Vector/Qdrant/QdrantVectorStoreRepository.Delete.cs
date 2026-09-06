namespace ApiDex.Infrastructure.Rag.Vector.Qdrant;

public partial class QdrantVectorStoreRepository
{
    public async Task DeleteByProjectIdAsync(Guid projectId, bool allModels = false,
        CancellationToken cancellationToken = default)
    {
        var models = allModels ? AllModels() : [await settingsManager.GetActiveModelAsync(cancellationToken)];
        foreach (var model in models)
        {
            await EnsureCollectionAsync(model, cancellationToken);
            await _client.DeleteAsync(
                CollectionName(model),
                CreateProjectFilter(projectId),
                wait: true,
                cancellationToken: cancellationToken);
        }
    }
}
