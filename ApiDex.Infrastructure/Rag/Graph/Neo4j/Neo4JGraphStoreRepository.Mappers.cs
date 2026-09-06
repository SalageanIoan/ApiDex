using ApiDex.Domain.DocumentationProjects;

namespace ApiDex.Infrastructure.Rag.Graph.Neo4j;

public partial class Neo4JGraphStoreRepository
{
    private static Dictionary<string, object?> ToGraphProject(DocumentationProject project)
    {
        return new Dictionary<string, object?>
        {
            ["id"] = project.Id.ToString(),
            ["title"] = project.Title,
            ["description"] = project.GeneralDescription,
            ["architecture"] = project.ArchitectureType.ToString()
        };
    }

    private static List<Dictionary<string, object?>> ToGraphServices(DocumentationProject project)
    {
        return project.Services.Select(service => new Dictionary<string, object?>
        {
            ["id"] = service.Id.ToString(),
            ["projectId"] = project.Id.ToString(),
            ["title"] = service.Title,
            ["description"] = service.GeneralDescription
        }).ToList();
    }

    private static List<Dictionary<string, object?>> ToGraphEndpoints(DocumentationProject project)
    {
        return project.Services.SelectMany(service => service.Endpoints.Select(endpoint =>
            new Dictionary<string, object?>
            {
                ["id"] = endpoint.Id.ToString(),
                ["projectId"] = project.Id.ToString(),
                ["serviceId"] = service.Id.ToString(),
                ["key"] = endpoint.Key,
                ["method"] = endpoint.HttpMethod,
                ["route"] = endpoint.Route,
                ["description"] = endpoint.Description
            })).ToList();
    }

    private static List<Dictionary<string, object?>> ToGraphEvents(DocumentationProject project)
    {
        return project.Services.SelectMany(service => service.Events.Select(serviceEvent =>
            new Dictionary<string, object?>
            {
                ["id"] = serviceEvent.Id.ToString(),
                ["projectId"] = project.Id.ToString(),
                ["serviceId"] = service.Id.ToString(),
                ["name"] = serviceEvent.Name,
                ["direction"] = serviceEvent.Direction.ToString(),
                ["description"] = serviceEvent.Description,
                ["endpointIds"] = serviceEvent.ServiceEndpointIds.Select(id => id.ToString()).ToList()
            })).ToList();
    }

    private static List<Dictionary<string, object?>> ToGraphDependencies(DocumentationProject project)
    {
        return project.Services.SelectMany(service => service.Dependencies.Select(dependency =>
            new Dictionary<string, object?>
            {
                ["id"] = dependency.Id.ToString(),
                ["projectId"] = project.Id.ToString(),
                ["serviceId"] = service.Id.ToString(),
                ["sourceEndpointId"] = dependency.SourceEndpointId.ToString(),
                ["targetEndpointId"] = dependency.TargetEndpointId?.ToString(),
                ["targetServiceTitle"] = dependency.TargetServiceTitle,
                ["targetEndpointRoute"] = dependency.TargetEndpointRoute,
                ["type"] = dependency.DependencyType.ToString(),
                ["description"] = dependency.Description
            })).ToList();
    }
}
