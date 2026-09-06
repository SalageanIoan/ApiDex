using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Application.Mediator.Commands;

namespace ApiDex.Application.DocumentationProjects.Commands.CreateDocumentationProject;

public sealed record CreateDocumentationProjectRequest(CreateDocumentationProjectIn Message) : ICommand
{
    public static CreateDocumentationProjectRequest FromMessage(CreateDocumentationProjectIn message)
    {
        return new CreateDocumentationProjectRequest(message);
    }
}
