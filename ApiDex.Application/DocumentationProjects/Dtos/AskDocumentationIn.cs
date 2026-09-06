namespace ApiDex.Application.DocumentationProjects.Dtos;

public class AskDocumentationIn
{
    public required string Question { get; init; }
    public Guid? ProjectId { get; init; }
    public Guid? ServiceId { get; init; }
}
