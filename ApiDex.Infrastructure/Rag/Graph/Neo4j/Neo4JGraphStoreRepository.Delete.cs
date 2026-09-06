namespace ApiDex.Infrastructure.Rag.Graph.Neo4j;

public partial class Neo4JGraphStoreRepository
{
    public async Task DeleteProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        await using var session = _driver.AsyncSession();
        await session.ExecuteWriteAsync(async tx =>
        {
            await tx.RunAsync(DeleteProjectQuery, new { projectId = projectId.ToString() });
        });
    }
}