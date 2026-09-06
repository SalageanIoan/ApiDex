using ApiDex.Domain.Rag;

namespace ApiDex.Infrastructure.Rag.Embeddings.Routing;

public partial class RoutingEmbeddingService(IRagSettingsManager settingsManager, IServiceProvider serviceProvider)
    : IEmbeddingService;
