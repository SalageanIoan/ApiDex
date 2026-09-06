using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetDocumentationProjectById;

public sealed record GetDocumentationProjectByIdRequest(Guid DocumentationProjectId)
    : IRequest<Result<DocumentationProjectOut>>
{
    public static GetDocumentationProjectByIdRequest FromId(Guid documentationProjectId)
    {
        return new GetDocumentationProjectByIdRequest(documentationProjectId);
    }
}
