using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.DocumentationProjects.Enums;
using ApiDex.Application.Mediator.Commands;
using ApiDex.Domain.Results;

namespace ApiDex.Application.DocumentationProjects.Commands.CreateServiceDependency;

public class CreateServiceDependencyHandler(IDocumentationProjectRepository documentationProjectRepository)
    : CommandHandler<CreateServiceDependencyRequest>
{
    public override async Task<Result> Handle(CreateServiceDependencyRequest command,
        CancellationToken cancellationToken)
    {
        var message = command.Message;

        var architectureType = await documentationProjectRepository.GetArchitectureTypeByServiceIdAsync(
            message.ServiceDocumentationId, cancellationToken);

        if (architectureType == ESystemArchitecture.Monolith)
        {
            var hasExternalTarget =
                message.TargetEndpointId is null &&
                !string.IsNullOrWhiteSpace(message.TargetServiceTitle) &&
                !string.IsNullOrWhiteSpace(message.TargetEndpointRoute);

            if (!hasExternalTarget)
            {
                return Error.Validation(
                    "ServiceDependency.MonolithExternalTargetRequired",
                    "Monolith projects only support dependencies to external targets.",
                    null!);
            }
        }

        await documentationProjectRepository.CreateServiceDependencyAsync(
            message.ServiceDocumentationId,
            message.SourceEndpointId,
            message.DependencyType,
            message.TargetEndpointId,
            message.TargetServiceTitle?.Trim(),
            message.TargetEndpointRoute?.Trim(),
            message.Description.Trim(),
            cancellationToken);

        return Result.Success();
    }
}
