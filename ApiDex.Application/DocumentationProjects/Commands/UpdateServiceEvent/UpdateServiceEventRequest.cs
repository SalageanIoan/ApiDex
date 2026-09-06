using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Commands.UpdateServiceEvent;

public sealed record UpdateServiceEventRequest(
    Guid ServiceEventId,
    UpdateServiceEventIn Message) : IRequest<Result>
{
    public static UpdateServiceEventRequest FromMessage(Guid serviceEventId, UpdateServiceEventIn message)
    {
        return new UpdateServiceEventRequest(serviceEventId, message);
    }
}
