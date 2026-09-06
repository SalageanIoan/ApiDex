using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.Mediator.Commands;

public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : IRequest<Result>
{
    public abstract Task<Result> Handle(TCommand command, CancellationToken cancellationToken);
}
