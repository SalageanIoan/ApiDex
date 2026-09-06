namespace ApiDex.Domain.Rag;

public sealed record EmbeddingModelDescriptor(
    EmbeddingModelType Type,
    string Key,
    int Dimensions);
