using ApiDex.Domain.DocumentationProjects;
using ApiDex.Application.Mediator.Commands;
using ApiDex.Domain.Results;

namespace ApiDex.Application.DocumentationProjects.Commands.UpdateServiceDependency;

public class UpdateServiceDependencyHandler(IDocumentationProjectRepository documentationProjectRepository)
    : CommandHandler<UpdateServiceDependencyRequest>
{
    public override async Task<Result> Handle(UpdateServiceDependencyRequest command,
        CancellationToken cancellationToken)
    {
        var message = command.Message;

        var updated = await documentationProjectRepository.UpdateServiceDependencyAsync(
            command.ServiceDependencyId,
            message.SourceEndpointId,
            message.DependencyType,
            message.TargetEndpointId,
            message.TargetServiceTitle?.Trim(),
            message.TargetEndpointRoute?.Trim(),
            message.Description.Trim(),
            cancellationToken);

        if (!updated)
        {
            return Error.NotFound(
                "ServiceDependency.NotFound",
                $"Service dependency '{command.ServiceDependencyId}' was not found.");
        }

        return Result.Success();
    }
}