using ApiDex.Domain.DocumentationProjects;

namespace ApiDex.Infrastructure.Rag.Graph.Neo4j;

public partial class Neo4JGraphStoreRepository
{
    public async Task SyncProjectAsync(DocumentationProject project, CancellationToken cancellationToken = default)
    {
        await EnsureSchemaAsync(cancellationToken);
        await DeleteProjectAsync(project.Id, cancellationToken);

        var graphProject = ToGraphProject(project);
        var services = ToGraphServices(project);
        var endpoints = ToGraphEndpoints(project);
        var events = ToGraphEvents(project);
        var dependencies = ToGraphDependencies(project);

        await using var session = _driver.AsyncSession();
        await session.ExecuteWriteAsync(async tx =>
        {
            await tx.RunAsync(ProjectMergeQuery, new { project = graphProject });
            await tx.RunAsync(ServiceMergeQuery, new { services });
            await tx.RunAsync(EndpointMergeQuery, new { endpoints });
            await tx.RunAsync(EventMergeQuery, new { events });
            await tx.RunAsync(DependencyMergeQuery, new { dependencies });
        });
    }
}