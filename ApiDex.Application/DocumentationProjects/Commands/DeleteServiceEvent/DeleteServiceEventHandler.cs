using ApiDex.Domain.DocumentationProjects;
using ApiDex.Application.Mediator.Commands;
using ApiDex.Domain.Results;

namespace ApiDex.Application.DocumentationProjects.Commands.DeleteServiceEvent;

public class DeleteServiceEventHandler(IDocumentationProjectRepository documentationProjectRepository)
    : CommandHandler<DeleteServiceEventRequest>
{
    public override async Task<Result> Handle(DeleteServiceEventRequest command,
        CancellationToken cancellationToken)
    {
        var deleted = await documentationProjectRepository.DeleteServiceEventAsync(
            command.ServiceEventId,
            cancellationToken);

        if (!deleted)
        {
            return Error.NotFound(
                "ServiceEvent.NotFound",
                $"Service event '{command.ServiceEventId}' was not found.");
        }

        return Result.Success();
    }
}