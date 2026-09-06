using ApiDex.Domain.Rag;
using ApiDex.Infrastructure.Rag.Embeddings.Local;
using ApiDex.Infrastructure.Rag.Embeddings.LocalBgeBase;
using ApiDex.Infrastructure.Rag.Embeddings.OpenAi;
using Microsoft.Extensions.DependencyInjection;

namespace ApiDex.Infrastructure.Rag.Embeddings.Routing;

public partial class RoutingEmbeddingService
{
    private async Task<IEmbeddingService> GetActiveServiceAsync(CancellationToken cancellationToken)
    {
        var activeModel = await settingsManager.GetActiveModelAsync(cancellationToken);
        return activeModel.Type switch
        {
            EmbeddingModelType.OpenAi => serviceProvider.GetRequiredService<OpenAiEmbeddingService>(),
            EmbeddingModelType.Local => serviceProvider.GetRequiredService<LocalEmbeddingService>(),
            EmbeddingModelType.LocalBgeBase => serviceProvider.GetRequiredService<LocalBgeBaseEmbeddingService>(),
            _ => throw new NotImplementedException()
        };
    }
}
