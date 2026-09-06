using ApiDex.Domain.DocumentationProjects.Enums;

namespace ApiDex.Infrastructure.Data.Entity;

public class DocumentationProjectEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string GeneralDescription { get; set; } = string.Empty;

    public ESystemArchitecture ArchitectureType { get; set; }

    public IList<ServiceDocumentationEntity> Services { get; set; } = [];
}