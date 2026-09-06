using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.DocumentationProjects.Enums;
using ApiDex.Application.Mediator.Commands;
using ApiDex.Domain.Results;

namespace ApiDex.Application.DocumentationProjects.Commands.CreateServiceEvent;

public class CreateServiceEventHandler(IDocumentationProjectRepository documentationProjectRepository)
    : CommandHandler<CreateServiceEventRequest>
{
    public override async Task<Result> Handle(CreateServiceEventRequest command,
        CancellationToken cancellationToken)
    {
        var message = command.Message;

        var architectureType = await documentationProjectRepository.GetArchitectureTypeByServiceIdAsync(
            message.ServiceDocumentationId, cancellationToken);

        if (architectureType == ESystemArchitecture.Monolith)
        {
            return Error.Validation(
                "ServiceEvent.MonolithRestriction",
                "Events are not supported for monolith projects. Events represent inter-service communication in distributed architectures.",
                null!);
        }

        await documentationProjectRepository.CreateServiceEventAsync(
            message.ServiceDocumentationId,
            message.ServiceEndpointIds,
            message.Name.Trim(),
            message.Description.Trim(),
            message.Direction,
            cancellationToken);

        return Result.Success();
    }
}
