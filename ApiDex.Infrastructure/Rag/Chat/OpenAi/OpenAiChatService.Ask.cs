using ApiDex.Domain.Rag;
using OpenAI.Chat;

namespace ApiDex.Infrastructure.Rag.Chat.OpenAi;

public partial class OpenAiChatService
{
    public async Task<ChatResponse> AskAsync(
        string systemPrompt,
        string userPrompt,
        CancellationToken cancellationToken = default)
    {
        ChatMessage[] messages =
        [
            new SystemChatMessage(systemPrompt),
            new UserChatMessage(userPrompt)
        ];

        var response = await _chatClient.CompleteChatAsync(
            messages,
            new ChatCompletionOptions(),
            cancellationToken);
        var answer = string.Concat(response.Value.Content.Select(part => part.Text));

        if (string.IsNullOrWhiteSpace(answer))
        {
            throw new InvalidOperationException("OpenAI returned an empty response.");
        }

        return new ChatResponse(answer, MapUsage(response.Value.Usage));
    }

    private static ChatTokenUsageMetadata? MapUsage(ChatTokenUsage? usage)
    {
        return usage is null
            ? null
            : new ChatTokenUsageMetadata(
                usage.InputTokenCount,
                usage.OutputTokenCount,
                usage.TotalTokenCount,
                usage.InputTokenDetails is null
                    ? null
                    : new ChatInputTokenUsageMetadata(
                        usage.InputTokenDetails.AudioTokenCount,
                        usage.InputTokenDetails.CachedTokenCount),
                usage.OutputTokenDetails is null
                    ? null
                    : new ChatOutputTokenUsageMetadata(
                        usage.OutputTokenDetails.AudioTokenCount,
                        usage.OutputTokenDetails.ReasoningTokenCount));
    }
}
