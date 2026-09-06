using ApiDex.Domain.DocumentationProjects.Enums;

namespace ApiDex.Domain.DocumentationProjects;

public class ServiceDependency
{
    public required Guid Id { get; init; }

    public required Guid SourceEndpointId { get; init; }

    public required EServiceDependencyType DependencyType { get; init; }

    public Guid? TargetEndpointId { get; init; }

    public string? TargetServiceTitle { get; init; }

    public string? TargetEndpointRoute { get; init; }

    public required string Description { get; init; }
}