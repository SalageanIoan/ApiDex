using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Commands.UpdateDocumentationProject;

public sealed record UpdateDocumentationProjectRequest(
    Guid DocumentationProjectId,
    UpdateDocumentationProjectIn Message) : IRequest<Result>
{
    public static UpdateDocumentationProjectRequest FromMessage(Guid documentationProjectId,
        UpdateDocumentationProjectIn message)
    {
        return new UpdateDocumentationProjectRequest(documentationProjectId, message);
    }
}
