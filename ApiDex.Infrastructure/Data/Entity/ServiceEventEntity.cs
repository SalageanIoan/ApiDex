using ApiDex.Domain.DocumentationProjects.Enums;

namespace ApiDex.Infrastructure.Data.Entity;

public class ServiceEventEntity
{
    public Guid Id { get; set; }

    public Guid ServiceDocumentationId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public EServiceEventDirection Direction { get; set; }

    public ServiceDocumentationEntity ServiceDocumentation { get; set; } = null!;

    public IList<ServiceEventEndpointEntity> EventEndpoints { get; set; } = [];
}
