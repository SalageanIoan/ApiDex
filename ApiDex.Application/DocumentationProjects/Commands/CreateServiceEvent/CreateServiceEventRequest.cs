using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Commands.CreateServiceEvent;

public sealed record CreateServiceEventRequest(CreateServiceEventIn Message) : IRequest<Result>
{
    public static CreateServiceEventRequest FromMessage(CreateServiceEventIn message)
    {
        return new CreateServiceEventRequest(message);
    }
}
