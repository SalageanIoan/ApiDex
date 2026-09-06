using ApiDex.Domain.Rag;

namespace ApiDex.Infrastructure.Rag.Graph.Neo4j;

public partial class Neo4JGraphStoreRepository
{
    public async Task<string> GetContextForChunksAsync(IReadOnlyList<RetrievedChunk> chunks, Guid? projectId = null,
        Guid? serviceId = null, RagContextMode contextMode = RagContextMode.Compact,
        CancellationToken cancellationToken = default)
    {
        var endpointIds = chunks
            .Select(chunk => chunk.EndpointId)
            .Where(id => id is not null)
            .Select(id => id!.Value.ToString())
            .Distinct()
            .ToList();

        var serviceIds = chunks
            .Select(chunk => chunk.ServiceId)
            .Where(id => id is not null)
            .Select(id => id!.Value.ToString())
            .Concat(serviceId is null ? [] : [serviceId.Value.ToString()])
            .Distinct()
            .ToList();

        if (endpointIds.Count == 0 && serviceIds.Count == 0 && projectId is null) return string.Empty;

        await using var session = _driver.AsyncSession();
        var cursor = await session.RunAsync(GetContextQuery(contextMode), new
        {
            projectId = projectId?.ToString(),
            serviceIds,
            endpointIds
        });

        var records = await cursor.ToListAsync();
        return records.Count == 0 ? string.Empty : BuildContext(records[0]);
    }
}
