using ApiDex.Domain.DocumentationProjects;
using ApiDex.Infrastructure.Data;
using ApiDex.Infrastructure.DocumentationProjects.Repositories;
using ApiDex.Domain.Rag;
using ApiDex.Infrastructure.Rag.Chat.OpenAi;
using ApiDex.Infrastructure.Rag.Context.EntityFramework;
using ApiDex.Infrastructure.Rag.Embeddings.Local;
using ApiDex.Infrastructure.Rag.Embeddings.LocalBgeBase;
using ApiDex.Infrastructure.Rag.Embeddings.OpenAi;
using ApiDex.Infrastructure.Rag.Embeddings.Routing;
using ApiDex.Infrastructure.Rag.Graph.Neo4j;
using ApiDex.Infrastructure.Rag.Settings;
using ApiDex.Infrastructure.Rag.Vector.Qdrant;
using Microsoft.EntityFrameworkCore;
using OpenAI;
using System.ClientModel;

namespace ApiDex.Server.DependencyInjection.Extensions;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ApiDexDb");

        services.AddDbContext<ApiDexDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IDocumentationProjectRepository, DocumentationProjectRepository>();

        services.AddScoped<IRagSettingsManager, RagSettingsManager>();

        services.AddSingleton<LocalEmbeddingService>();
        services.AddSingleton<LocalBgeBaseEmbeddingService>();

        var openAiApiKey = configuration["OpenAI:ApiKey"];
        if (string.IsNullOrWhiteSpace(openAiApiKey))
        {
            throw new InvalidOperationException("OpenAI:ApiKey is not configured.");
        }

        var openAiBaseUrl = configuration["OpenAI:BaseUrl"] ?? "https://api.openai.com/v1";
        services.AddSingleton(new OpenAIClient(
            new ApiKeyCredential(openAiApiKey),
            new OpenAIClientOptions
            {
                Endpoint = new Uri(openAiBaseUrl, UriKind.Absolute)
            }));

        services.AddSingleton<OpenAiEmbeddingService>();
        services.AddScoped<IEmbeddingService, RoutingEmbeddingService>();
        services.AddSingleton<IChatService, OpenAiChatService>();

        services.AddScoped<IVectorStoreRepository, QdrantVectorStoreRepository>();
        services.AddScoped<IRagChunkRepository, EfRagChunkRepository>();

        services.AddSingleton<IGraphStoreRepository, Neo4JGraphStoreRepository>();
    }
}
