using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceEndpointsByServiceDocumentationId;

public sealed record GetServiceEndpointsByServiceDocumentationIdRequest(Guid ServiceDocumentationId)
    : IRequest<Result<List<ServiceEndpointOut>>>
{
    public static GetServiceEndpointsByServiceDocumentationIdRequest FromId(Guid serviceDocumentationId)
    {
        return new GetServiceEndpointsByServiceDocumentationIdRequest(serviceDocumentationId);
    }
}
