using ApiDex.Domain.DocumentationProjects.Enums;

namespace ApiDex.Domain.DocumentationProjects;

public class DocumentationProject
{
    public required Guid Id { get; init; }

    public required string Title { get; init; }

    public required string GeneralDescription { get; init; }

    public required ESystemArchitecture ArchitectureType { get; init; }

    public required IList<ServiceDocumentation> Services { get; init; }
}