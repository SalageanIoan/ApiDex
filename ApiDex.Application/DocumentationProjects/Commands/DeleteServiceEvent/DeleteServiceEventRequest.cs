using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Commands.DeleteServiceEvent;

public sealed record DeleteServiceEventRequest(Guid ServiceEventId) : IRequest<Result>
{
    public static DeleteServiceEventRequest FromId(Guid serviceEventId)
    {
        return new DeleteServiceEventRequest(serviceEventId);
    }
}
