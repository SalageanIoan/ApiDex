using ApiDex.Domain.Rag;
using ApiDex.Infrastructure.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.Rag.Settings;

public partial class RagSettingsManager
{
    public async Task EnsureInitializedAsync(CancellationToken cancellationToken = default)
    {
        var settings = await dbContext.RagSettings
            .SingleOrDefaultAsync(settings => settings.Id == DefaultSettingsId, cancellationToken);

        if (settings is not null)
        {
            var changed = false;
            if (string.IsNullOrWhiteSpace(settings.ActiveModel))
            {
                settings.ActiveModel = EmbeddingModelType.OpenAi.ToString();
                changed = true;
            }

            if (string.IsNullOrWhiteSpace(settings.ContextMode))
            {
                settings.ContextMode = RagContextMode.Compact.ToString();
                changed = true;
            }

            if (settings.TopKChunks <= 0)
            {
                settings.TopKChunks = RagContextDefaults.GetTopKChunks(ParseContextMode(settings.ContextMode));
                changed = true;
            }

            if (changed)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            return;
        }

        dbContext.RagSettings.Add(new RagSettingsEntity
        {
            Id = DefaultSettingsId,
            ActiveModel = EmbeddingModelType.OpenAi.ToString(),
            ContextMode = RagContextMode.Compact.ToString(),
            TopKChunks = RagContextDefaults.GetTopKChunks(RagContextMode.Compact)
        });

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
