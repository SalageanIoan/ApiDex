using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceEventById;

public sealed record GetServiceEventByIdRequest(Guid ServiceEventId)
    : IRequest<Result<ServiceEventOut>>
{
    public static GetServiceEventByIdRequest FromId(Guid serviceEventId)
    {
        return new GetServiceEventByIdRequest(serviceEventId);
    }
}
