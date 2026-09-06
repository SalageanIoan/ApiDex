namespace ApiDex.Infrastructure.Data.Entity;

public class ServiceEndpointEntity
{
    public Guid Id { get; set; }

    public Guid ServiceDocumentationId { get; set; }

    public string Key { get; set; } = string.Empty;

    public string HttpMethod { get; set; } = string.Empty;

    public string Route { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public ServiceDocumentationEntity ServiceDocumentation { get; set; } = null!;

    public IList<ServiceDependencyEntity> SourceDependencies { get; set; } = [];

    public IList<ServiceDependencyEntity> TargetDependencies { get; set; } = [];

    public IList<ServiceEventEndpointEntity> EventEndpoints { get; set; } = [];
}
