namespace ApiDex.Domain.Rag;

public interface IChatService
{
    Task<ChatResponse> AskAsync(string systemPrompt, string userPrompt, CancellationToken cancellationToken = default);
}

public sealed record ChatResponse(string Answer, ChatTokenUsageMetadata? Usage);

public sealed record ChatTokenUsageMetadata(
    int InputTokenCount,
    int OutputTokenCount,
    int TotalTokenCount,
    ChatInputTokenUsageMetadata? InputTokenDetails,
    ChatOutputTokenUsageMetadata? OutputTokenDetails);

public sealed record ChatInputTokenUsageMetadata(
    int AudioTokenCount,
    int CachedTokenCount);

public sealed record ChatOutputTokenUsageMetadata(
    int AudioTokenCount,
    int ReasoningTokenCount);
