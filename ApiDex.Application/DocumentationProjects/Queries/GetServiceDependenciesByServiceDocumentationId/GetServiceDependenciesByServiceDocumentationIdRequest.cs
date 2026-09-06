using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceDependenciesByServiceDocumentationId;

public sealed record GetServiceDependenciesByServiceDocumentationIdRequest(Guid ServiceDocumentationId)
    : IRequest<Result<List<ServiceDependencyOut>>>
{
    public static GetServiceDependenciesByServiceDocumentationIdRequest FromId(
        Guid serviceDocumentationId)
    {
        return new GetServiceDependenciesByServiceDocumentationIdRequest(serviceDocumentationId);
    }
}
