namespace ApiDex.Infrastructure.Data.Entity;

public class ServiceDocumentationEntity
{
    public Guid Id { get; set; }

    public Guid DocumentationProjectId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string GeneralDescription { get; set; } = string.Empty;

    public DocumentationProjectEntity DocumentationProject { get; set; } = null!;

    public IList<ServiceEndpointEntity> Endpoints { get; set; } = [];

    public IList<ServiceEventEntity> Events { get; set; } = [];

    public IList<ServiceDependencyEntity> Dependencies { get; set; } = [];
}