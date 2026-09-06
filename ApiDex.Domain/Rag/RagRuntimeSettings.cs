namespace ApiDex.Domain.Rag;

public record RagRuntimeSettings(
    EmbeddingModelDescriptor ActiveModel,
    RagContextMode ContextMode,
    int TopKChunks);