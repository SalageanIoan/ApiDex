using ApiDex.Domain.Rag;
using Microsoft.Extensions.Configuration;
using Qdrant.Client;

namespace ApiDex.Infrastructure.Rag.Vector.Qdrant;

public partial class QdrantVectorStoreRepository(
    IConfiguration configuration,
    IRagSettingsManager settingsManager) : IVectorStoreRepository, IDisposable
{
    private readonly string _collectionPrefix = configuration["Qdrant:CollectionPrefix"] ?? "apidex_docs";
    private readonly QdrantClient _client = CreateClient(configuration);

    private static QdrantClient CreateClient(IConfiguration configuration)
    {
        var host = configuration["Qdrant:GrpcHost"] ?? "localhost";
        var port = int.TryParse(configuration["Qdrant:GrpcPort"], out var configuredPort)
            ? configuredPort
            : 6334;
        var https = bool.TryParse(configuration["Qdrant:Https"], out var configuredHttps) && configuredHttps;
        var apiKey = configuration["Qdrant:ApiKey"];

        return new QdrantClient(host, port, https, apiKey);
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}
