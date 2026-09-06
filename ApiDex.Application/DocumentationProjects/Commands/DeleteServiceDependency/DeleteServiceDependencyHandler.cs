using ApiDex.Domain.DocumentationProjects;
using ApiDex.Application.Mediator.Commands;
using ApiDex.Domain.Results;

namespace ApiDex.Application.DocumentationProjects.Commands.DeleteServiceDependency;

public class DeleteServiceDependencyHandler(IDocumentationProjectRepository documentationProjectRepository)
    : CommandHandler<DeleteServiceDependencyRequest>
{
    public override async Task<Result> Handle(DeleteServiceDependencyRequest command,
        CancellationToken cancellationToken)
    {
        var deleted = await documentationProjectRepository.DeleteServiceDependencyAsync(
            command.ServiceDependencyId,
            cancellationToken);

        if (!deleted)
        {
            return Error.NotFound(
                "ServiceDependency.NotFound",
                $"Service dependency '{command.ServiceDependencyId}' was not found.");
        }

        return Result.Success();
    }
}