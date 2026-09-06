using ApiDex.Domain.DocumentationProjects.Enums;

namespace ApiDex.Application.DocumentationProjects.Dtos;

public class UpdateDocumentationProjectIn
{
    public string Title { get; init; } = string.Empty;

    public string GeneralDescription { get; init; } = string.Empty;

    public ESystemArchitecture ArchitectureType { get; init; }
}