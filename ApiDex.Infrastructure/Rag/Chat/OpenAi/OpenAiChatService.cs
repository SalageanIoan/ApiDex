using ApiDex.Domain.Rag;
using Microsoft.Extensions.Configuration;
using OpenAI;
using OpenAI.Chat;

namespace ApiDex.Infrastructure.Rag.Chat.OpenAi;

public partial class OpenAiChatService : IChatService
{
    private readonly ChatClient _chatClient;

    public OpenAiChatService(OpenAIClient openAiClient, IConfiguration configuration)
    {
        var model = configuration["OpenAI:ChatModel"] ??
                    throw new InvalidOperationException("OpenAI:ChatModel is not configured.");

        _chatClient = openAiClient.GetChatClient(model);
    }
}
