using System.Text;
using ApiDex.Domain.DocumentationProjects;

namespace ApiDex.Domain.Rag;

public static class DocumentationChunkFactory
{
    public static List<DocumentationChunk> CreateAll(DocumentationProject project)
    {
        var chunks = new List<DocumentationChunk>
        {
            CreateProject(project)
        };

        foreach (var service in project.Services)
        {
            chunks.Add(CreateService(project.Id, project.Title, service));
            chunks.AddRange(service.Endpoints.Select(endpoint =>
                CreateEndpoint(project.Id, project.Title, service, endpoint)));
        }

        return chunks;
    }

    public static DocumentationChunk CreateProject(DocumentationProject project)
    {
        return new DocumentationChunk
        {
            Id = $"proj_{project.Id}",
            Content =
                $"Project Title: {project.Title}\nArchitecture: {project.ArchitectureType}\nGeneral Description: {project.GeneralDescription}",
            ProjectId = project.Id,
            Source = "project"
        };
    }

    public static DocumentationChunk CreateService(
        Guid projectId,
        string projectTitle,
        ServiceDocumentation service)
    {
        var content = new StringBuilder()
            .AppendLine($"Project: {projectTitle}")
            .AppendLine($"Service: {service.Title}")
            .AppendLine($"Description: {service.GeneralDescription}");

        AppendEndpoints(content, service);
        AppendEvents(content, service);
        AppendDependencies(content, service);

        return new DocumentationChunk
        {
            Id = $"svc_{service.Id}",
            Content = content.ToString(),
            ProjectId = projectId,
            ServiceId = service.Id,
            Source = "service"
        };
    }

    public static DocumentationChunk CreateEndpoint(
        Guid projectId,
        string projectTitle,
        ServiceDocumentation service,
        ServiceEndpoint endpoint)
    {
        var content = new StringBuilder()
            .AppendLine($"Project: {projectTitle}")
            .AppendLine($"Service: {service.Title}")
            .AppendLine($"Endpoint: {endpoint.HttpMethod} {endpoint.Route}")
            .AppendLine($"Key: {endpoint.Key}")
            .AppendLine($"Description: {endpoint.Description}");

        var relatedEvents = service.Events
            .Where(serviceEvent => serviceEvent.ServiceEndpointIds.Contains(endpoint.Id))
            .ToList();

        if (relatedEvents.Count > 0)
        {
            content.AppendLine("Related Events:");
            foreach (var serviceEvent in relatedEvents)
            {
                content.AppendLine(
                    $"- {serviceEvent.Name} ({serviceEvent.Direction}): {serviceEvent.Description}");
            }
        }

        var relatedDependencies = service.Dependencies
            .Where(dependency => dependency.SourceEndpointId == endpoint.Id)
            .ToList();

        if (relatedDependencies.Count > 0)
        {
            content.AppendLine("Related Dependencies:");
            foreach (var dependency in relatedDependencies)
            {
                content.AppendLine(
                    $"- Calls {dependency.TargetServiceTitle ?? "external"} at {dependency.TargetEndpointRoute ?? "unknown"} for {dependency.DependencyType}. {dependency.Description}");
            }
        }

        return new DocumentationChunk
        {
            Id = $"ep_{endpoint.Id}",
            Content = content.ToString(),
            ProjectId = projectId,
            ServiceId = service.Id,
            EndpointId = endpoint.Id,
            Source = "endpoint"
        };
    }

    private static void AppendEndpoints(StringBuilder content, ServiceDocumentation service)
    {
        if (service.Endpoints.Count == 0) return;

        content.AppendLine("Endpoints:");
        foreach (var endpoint in service.Endpoints)
        {
            content.AppendLine($"- {endpoint.HttpMethod} {endpoint.Route}: {endpoint.Key}");
        }
    }

    private static void AppendEvents(StringBuilder content, ServiceDocumentation service)
    {
        if (service.Events.Count == 0) return;

        content.AppendLine("Events:");
        foreach (var serviceEvent in service.Events)
        {
            content.AppendLine(
                $"- {serviceEvent.Name} ({serviceEvent.Direction}): {serviceEvent.Description}");
        }
    }

    private static void AppendDependencies(StringBuilder content, ServiceDocumentation service)
    {
        if (service.Dependencies.Count == 0) return;

        content.AppendLine("Dependencies:");
        foreach (var dependency in service.Dependencies)
        {
            content.AppendLine(
                $"- {dependency.DependencyType} to {dependency.TargetServiceTitle ?? "external"} at {dependency.TargetEndpointRoute ?? "unknown"}. Description: {dependency.Description}");
        }
    }
}
