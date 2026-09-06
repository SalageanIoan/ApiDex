namespace ApiDex.Domain.Rag;

public interface IRagSettingsManager
{
    Task<EmbeddingModelDescriptor> GetActiveModelAsync(CancellationToken cancellationToken = default);

    Task<RagRuntimeSettings> GetSettingsAsync(CancellationToken cancellationToken = default);

    Task UpdateSettingsAsync(EmbeddingModelType activeModel, RagContextMode contextMode,
        CancellationToken cancellationToken = default);

    Task EnsureInitializedAsync(CancellationToken cancellationToken = default);
}
