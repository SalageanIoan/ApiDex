using ApiDex.Domain.DocumentationProjects.Enums;

namespace ApiDex.Application.DocumentationProjects.Dtos;

public class DocumentationProjectOut
{
    public Guid Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public string GeneralDescription { get; init; } = string.Empty;

    public ESystemArchitecture ArchitectureType { get; init; }

    public List<ServiceDocumentationOut> Services { get; init; } = [];
}