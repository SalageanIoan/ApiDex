using ApiDex.Domain.DocumentationProjects.Enums;

namespace ApiDex.Application.DocumentationProjects.Dtos;

public class ServiceEventOut
{
    public Guid Id { get; init; }

    public List<Guid> ServiceEndpointIds { get; init; } = [];

    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public EServiceEventDirection Direction { get; init; }
}
