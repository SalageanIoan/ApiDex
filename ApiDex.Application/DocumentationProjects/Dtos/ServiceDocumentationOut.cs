namespace ApiDex.Application.DocumentationProjects.Dtos;

public class ServiceDocumentationOut
{
    public Guid Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public string GeneralDescription { get; init; } = string.Empty;

    public List<ServiceEndpointOut> Endpoints { get; init; } = [];

    public List<ServiceEventOut> Events { get; init; } = [];

    public List<ServiceDependencyOut> Dependencies { get; init; } = [];
}