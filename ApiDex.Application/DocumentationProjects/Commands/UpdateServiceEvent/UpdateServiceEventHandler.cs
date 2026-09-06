using ApiDex.Domain.DocumentationProjects;
using ApiDex.Application.Mediator.Commands;
using ApiDex.Domain.Results;

namespace ApiDex.Application.DocumentationProjects.Commands.UpdateServiceEvent;

public class UpdateServiceEventHandler(IDocumentationProjectRepository documentationProjectRepository)
    : CommandHandler<UpdateServiceEventRequest>
{
    public override async Task<Result> Handle(UpdateServiceEventRequest command,
        CancellationToken cancellationToken)
    {
        var message = command.Message;

        var updated = await documentationProjectRepository.UpdateServiceEventAsync(
            command.ServiceEventId,
            message.ServiceEndpointIds,
            message.Name.Trim(),
            message.Description.Trim(),
            message.Direction,
            cancellationToken);

        if (!updated)
        {
            return Error.NotFound(
                "ServiceEvent.NotFound",
                $"Service event '{command.ServiceEventId}' was not found.");
        }

        return Result.Success();
    }
}
