using ApiDex.Domain.DocumentationProjects.Enums;

namespace ApiDex.Infrastructure.Data.Entity;

public class ServiceDependencyEntity
{
    public Guid Id { get; set; }

    public Guid ServiceDocumentationId { get; set; }

    public Guid SourceEndpointId { get; set; }

    public EServiceDependencyType DependencyType { get; set; }

    public Guid? TargetEndpointId { get; set; }

    public string? TargetServiceTitle { get; set; }

    public string? TargetEndpointRoute { get; set; }

    public string Description { get; set; } = string.Empty;

    public ServiceDocumentationEntity ServiceDocumentation { get; set; } = null!;

    public ServiceEndpointEntity SourceEndpoint { get; set; } = null!;

    public ServiceEndpointEntity? TargetEndpoint { get; set; }
}