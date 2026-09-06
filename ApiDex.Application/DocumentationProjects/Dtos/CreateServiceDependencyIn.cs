using ApiDex.Domain.DocumentationProjects.Enums;

namespace ApiDex.Application.DocumentationProjects.Dtos;

public class CreateServiceDependencyIn
{
    public Guid ServiceDocumentationId { get; init; }

    public Guid SourceEndpointId { get; init; }

    public EServiceDependencyType DependencyType { get; init; }

    public Guid? TargetEndpointId { get; init; }

    public string? TargetServiceTitle { get; init; }

    public string? TargetEndpointRoute { get; init; }

    public string Description { get; init; } = string.Empty;
}