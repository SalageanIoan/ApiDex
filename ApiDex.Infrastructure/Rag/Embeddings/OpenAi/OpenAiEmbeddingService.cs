using ApiDex.Domain.Rag;
using ApiDex.Infrastructure.Rag.Settings;
using OpenAI;
using OpenAI.Embeddings;

namespace ApiDex.Infrastructure.Rag.Embeddings.OpenAi;

public partial class OpenAiEmbeddingService(
    OpenAIClient openAiClient) : IEmbeddingService
{
    private readonly string _model = RagSettingsManager.ToDescriptor(EmbeddingModelType.OpenAi).Key;

    private EmbeddingClient EmbeddingClient => openAiClient.GetEmbeddingClient(_model);
}
