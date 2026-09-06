using ApiDex.Domain.Rag;
using Microsoft.Extensions.Configuration;
using Neo4j.Driver;

namespace ApiDex.Infrastructure.Rag.Graph.Neo4j;

public partial class Neo4JGraphStoreRepository(IConfiguration configuration) : IGraphStoreRepository, IAsyncDisposable
{
    private readonly IDriver _driver = GraphDatabase.Driver(
        configuration["Neo4j:Uri"] ?? "bolt://localhost:7687",
        AuthTokens.Basic(
            configuration["Neo4j:Username"] ?? "neo4j",
            configuration["Neo4j:Password"] ?? "password"));

    public async ValueTask DisposeAsync()
    {
        await _driver.DisposeAsync();
    }
}