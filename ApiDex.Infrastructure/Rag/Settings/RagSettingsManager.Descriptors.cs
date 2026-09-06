using ApiDex.Domain.Rag;

namespace ApiDex.Infrastructure.Rag.Settings;

public partial class RagSettingsManager
{
    public static EmbeddingModelDescriptor ToDescriptor(EmbeddingModelType model)
    {
        return EmbeddingModelDescriptors.Get(model);
    }

    private static EmbeddingModelType Parse(string value)
    {
        return Enum.TryParse<EmbeddingModelType>(value, out var model) ? model : EmbeddingModelType.OpenAi;
    }

    private static RagContextMode ParseContextMode(string value)
    {
        if (string.Equals(value, "FullDebug", StringComparison.OrdinalIgnoreCase))
        {
            return RagContextMode.Comprehensive;
        }

        return Enum.TryParse<RagContextMode>(value, out var mode) ? mode : RagContextMode.Compact;
    }
}
