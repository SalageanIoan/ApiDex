namespace ApiDex.Application.Rag.Dtos;

public sealed record RagSettingsOut(
    string ActiveModel,
    string ContextMode,
    IReadOnlyList<string> AvailableModels,
    IReadOnlyList<string> AvailableContextModes,
    IReadOnlyList<RagModelDescriptorOut> ModelDescriptors);
