using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Commands.DeleteDocumentationProject;

public sealed record DeleteDocumentationProjectRequest(Guid DocumentationProjectId) : IRequest<Result>
{
    public static DeleteDocumentationProjectRequest FromId(Guid documentationProjectId)
    {
        return new DeleteDocumentationProjectRequest(documentationProjectId);
    }
}
