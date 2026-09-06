using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.DocumentationProjects.Enums;
using ApiDex.Application.Mediator.Commands;
using ApiDex.Domain.Results;

namespace ApiDex.Application.DocumentationProjects.Commands.DeleteServiceDocumentation;

public class DeleteServiceDocumentationHandler(IDocumentationProjectRepository documentationProjectRepository)
    : CommandHandler<DeleteServiceDocumentationRequest>
{
    public override async Task<Result> Handle(DeleteServiceDocumentationRequest command,
        CancellationToken cancellationToken)
    {
        var architectureType = await documentationProjectRepository.GetArchitectureTypeByServiceIdAsync(
            command.ServiceDocumentationId, cancellationToken);

        if (architectureType == ESystemArchitecture.Monolith)
        {
            return Error.Validation(
                "ServiceDocumentation.MonolithRestriction",
                "Cannot delete the implicit service of a monolith project. Delete the project instead.",
                null!);
        }

        var deleted = await documentationProjectRepository.DeleteServiceDocumentationAsync(
            command.ServiceDocumentationId,
            cancellationToken);

        if (!deleted)
        {
            return Error.NotFound(
                "ServiceDocumentation.NotFound",
                $"Service documentation '{command.ServiceDocumentationId}' was not found.");
        }

        return Result.Success();
    }
}