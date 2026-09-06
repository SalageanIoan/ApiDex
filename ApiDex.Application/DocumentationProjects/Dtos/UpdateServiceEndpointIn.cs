namespace ApiDex.Application.DocumentationProjects.Dtos;

public class UpdateServiceEndpointIn
{
    public string HttpMethod { get; init; } = string.Empty;

    public string Route { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
}