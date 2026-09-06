using System.Text;
using ApiDex.Domain.Rag;
using MediatR;

namespace ApiDex.Application.Rag.Queries.AskDocumentation;

public class AskDocumentationQueryHandler(
    IEmbeddingService embeddingService,
    IVectorStoreRepository vectorStoreRepository,
    IRagChunkRepository ragChunkRepository,
    IGraphStoreRepository graphStoreRepository,
    IRagSettingsManager settingsManager,
    IChatService chatService) : IRequestHandler<AskDocumentationQuery, AskDocumentationResult>
{
    private const string SystemPrompt = "You are an AI documentation assistant for a software architecture mapping tool called ApiDex. Answer the user's question using the provided context. If the exact answer is not in the context, but there are related details, summarize what you DO know about the project based on the context. If the context is completely empty, say you don't know.";

    public async Task<AskDocumentationResult> Handle(AskDocumentationQuery request, CancellationToken cancellationToken)
    {
        var settings = await settingsManager.GetSettingsAsync(cancellationToken);
        var questionEmbedding = await embeddingService.EmbedAsync(request.Question, cancellationToken);

        var vectorMatches = await vectorStoreRepository.SearchAsync(
            questionEmbedding,
            settings.TopKChunks,
            request.ProjectId,
            request.ServiceId,
            cancellationToken);

        var retrievedChunks = await ragChunkRepository.HydrateAsync(vectorMatches, cancellationToken);

        if (retrievedChunks.Count == 0)
        {
            return new AskDocumentationResult("I can't find this in documentation.", null);
        }

        var contextBuilder = new StringBuilder();
        foreach (var chunk in retrievedChunks)
        {
            contextBuilder.AppendLine("---");
            contextBuilder.AppendLine(chunk.Content);
        }

        var graphContext = await graphStoreRepository.GetContextForChunksAsync(
            retrievedChunks,
            request.ProjectId,
            request.ServiceId,
            settings.ContextMode,
            cancellationToken);

        if (!string.IsNullOrWhiteSpace(graphContext))
        {
            contextBuilder.AppendLine("---");
            contextBuilder.AppendLine("Graph context:");
            contextBuilder.AppendLine(graphContext);
        }

        var userPrompt = $"""
            Context:
            {contextBuilder}
            
            Do not assume anything not present in the context.
            
            Question: {request.Question}
            """;

        var response = await chatService.AskAsync(SystemPrompt, userPrompt, cancellationToken);
        return new AskDocumentationResult(response.Answer, response.Usage);
    }
}
