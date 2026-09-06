using ApiDex.Domain.Rag;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.Rag.Settings;

public partial class RagSettingsManager
{
    public async Task<EmbeddingModelDescriptor> GetActiveModelAsync(CancellationToken cancellationToken = default)
    {
        var settings = await GetSettingsAsync(cancellationToken);
        return settings.ActiveModel;
    }

    public async Task<RagRuntimeSettings> GetSettingsAsync(CancellationToken cancellationToken = default)
    {
        await EnsureInitializedAsync(cancellationToken);

        var settings = await dbContext.RagSettings
            .SingleAsync(settings => settings.Id == DefaultSettingsId, cancellationToken);

        return new RagRuntimeSettings(
            ToDescriptor(Parse(settings.ActiveModel)),
            ParseContextMode(settings.ContextMode),
            RagContextDefaults.GetTopKChunks(ParseContextMode(settings.ContextMode)));
    }

    public async Task SetActiveModelAsync(EmbeddingModelType model, CancellationToken cancellationToken = default)
    {
        var settings = await GetSettingsAsync(cancellationToken);
        await UpdateSettingsAsync(model, settings.ContextMode, cancellationToken);
    }

    public async Task UpdateSettingsAsync(EmbeddingModelType activeModel, RagContextMode contextMode,
        CancellationToken cancellationToken = default)
    {
        await EnsureInitializedAsync(cancellationToken);

        var settings = await dbContext.RagSettings
            .SingleAsync(settings => settings.Id == DefaultSettingsId, cancellationToken);

        settings.ActiveModel = activeModel.ToString();
        settings.ContextMode = contextMode.ToString();
        settings.TopKChunks = RagContextDefaults.GetTopKChunks(contextMode);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
