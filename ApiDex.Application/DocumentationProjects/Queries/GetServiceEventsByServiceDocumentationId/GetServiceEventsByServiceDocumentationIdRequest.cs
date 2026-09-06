using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceEventsByServiceDocumentationId;

public sealed record GetServiceEventsByServiceDocumentationIdRequest(Guid ServiceDocumentationId)
    : IRequest<Result<List<ServiceEventOut>>>
{
    public static GetServiceEventsByServiceDocumentationIdRequest FromId(Guid serviceDocumentationId)
    {
        return new GetServiceEventsByServiceDocumentationIdRequest(serviceDocumentationId);
    }
}
