namespace ApiDex.Domain.DocumentationProjects;

public class ServiceDocumentation
{
    public required Guid Id { get; init; }

    public required string Title { get; init; }

    public required string GeneralDescription { get; init; }

    public required IList<ServiceEndpoint> Endpoints { get; init; }

    public required IList<ServiceEvent> Events { get; init; }

    public required IList<ServiceDependency> Dependencies { get; init; }
}