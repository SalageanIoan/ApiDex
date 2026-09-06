namespace ApiDex.Domain.Rag;

public static class EmbeddingModelDescriptors
{
    public static IReadOnlyList<EmbeddingModelDescriptor> All { get; } =
        Enum.GetValues<EmbeddingModelType>().Select(Get).ToList();

    public static EmbeddingModelDescriptor Get(EmbeddingModelType model)
    {
        return model switch
        {
            EmbeddingModelType.OpenAi => new EmbeddingModelDescriptor(model, "text-embedding-3-small", 1536),
            EmbeddingModelType.Local => new EmbeddingModelDescriptor(model, "local-bge-micro-v2", 384),
            EmbeddingModelType.LocalBgeBase => new EmbeddingModelDescriptor(model, "local-bge-base-en-v1.5", 768),
            _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
        };
    }
}
