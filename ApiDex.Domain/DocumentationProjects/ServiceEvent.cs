using ApiDex.Domain.DocumentationProjects.Enums;

namespace ApiDex.Domain.DocumentationProjects;

public class ServiceEvent
{
    public required Guid Id { get; init; }

    public required IList<Guid> ServiceEndpointIds { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required EServiceEventDirection Direction { get; init; }
}
