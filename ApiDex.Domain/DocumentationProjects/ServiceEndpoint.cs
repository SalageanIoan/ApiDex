namespace ApiDex.Domain.DocumentationProjects;

public class ServiceEndpoint
{
    public required Guid Id { get; init; }

    public required Guid ServiceDocumentationId { get; init; }

    public required string ServiceTitle { get; init; }

    public required string Key { get; init; }

    public required string HttpMethod { get; init; }

    public required string Route { get; init; }

    public required string Description { get; init; }
}