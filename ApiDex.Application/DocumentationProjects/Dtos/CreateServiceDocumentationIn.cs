namespace ApiDex.Application.DocumentationProjects.Dtos;

public class CreateServiceDocumentationIn
{
    public Guid DocumentationProjectId { get; init; }

    public string Title { get; init; } = string.Empty;

    public string GeneralDescription { get; init; } = string.Empty;
}