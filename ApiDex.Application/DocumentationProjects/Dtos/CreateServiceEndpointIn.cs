namespace ApiDex.Application.DocumentationProjects.Dtos;

public class CreateServiceEndpointIn
{
    public Guid ServiceDocumentationId { get; init; }

    public string Key { get; init; } = string.Empty;

    public string HttpMethod { get; init; } = string.Empty;

    public string Route { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
}