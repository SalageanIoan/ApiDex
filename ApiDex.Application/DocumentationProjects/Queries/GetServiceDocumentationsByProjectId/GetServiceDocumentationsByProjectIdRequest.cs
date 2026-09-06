using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceDocumentationsByProjectId;

public sealed record GetServiceDocumentationsByProjectIdRequest(Guid DocumentationProjectId)
    : IRequest<Result<List<ServiceDocumentationOut>>>
{
    public static GetServiceDocumentationsByProjectIdRequest FromId(Guid documentationProjectId)
    {
        return new GetServiceDocumentationsByProjectIdRequest(documentationProjectId);
    }
}
