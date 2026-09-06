using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceDocumentationById;

public sealed record GetServiceDocumentationByIdRequest(Guid ServiceDocumentationId)
    : IRequest<Result<ServiceDocumentationOut>>
{
    public static GetServiceDocumentationByIdRequest FromId(Guid serviceDocumentationId)
    {
        return new GetServiceDocumentationByIdRequest(serviceDocumentationId);
    }
}
