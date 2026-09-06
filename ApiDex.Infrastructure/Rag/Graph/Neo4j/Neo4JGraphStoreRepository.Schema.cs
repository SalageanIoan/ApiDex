namespace ApiDex.Infrastructure.Rag.Graph.Neo4j;

public partial class Neo4JGraphStoreRepository
{
    public async Task EnsureSchemaAsync(CancellationToken cancellationToken = default)
    {
        await using var session = _driver.AsyncSession();
        await session.ExecuteWriteAsync(async tx =>
        {
            foreach (var query in ConstraintQueries) await tx.RunAsync(query);
        });
    }

    private static readonly string[] ConstraintQueries =
    [
        "CREATE CONSTRAINT apidex_project_id IF NOT EXISTS FOR (p:Project) REQUIRE p.id IS UNIQUE",
        "CREATE CONSTRAINT apidex_service_id IF NOT EXISTS FOR (s:Service) REQUIRE s.id IS UNIQUE",
        "CREATE CONSTRAINT apidex_endpoint_id IF NOT EXISTS FOR (e:Endpoint) REQUIRE e.id IS UNIQUE",
        "CREATE CONSTRAINT apidex_event_id IF NOT EXISTS FOR (e:Event) REQUIRE e.id IS UNIQUE",
        "CREATE CONSTRAINT apidex_dependency_id IF NOT EXISTS FOR (d:Dependency) REQUIRE d.id IS UNIQUE"
    ];
}
