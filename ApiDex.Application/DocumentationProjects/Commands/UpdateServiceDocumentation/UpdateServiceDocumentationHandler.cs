using ApiDex.Domain.DocumentationProjects;
using ApiDex.Application.Mediator.Commands;
using ApiDex.Domain.Results;

namespace ApiDex.Application.DocumentationProjects.Commands.UpdateServiceDocumentation;

public class UpdateServiceDocumentationHandler(IDocumentationProjectRepository documentationProjectRepository)
    : CommandHandler<UpdateServiceDocumentationRequest>
{
    public override async Task<Result> Handle(UpdateServiceDocumentationRequest command,
        CancellationToken cancellationToken)
    {
        var message = command.Message;

        var updated = await documentationProjectRepository.UpdateServiceDocumentationAsync(
            command.ServiceDocumentationId,
            message.Title.Trim(),
            message.GeneralDescription.Trim(),
            cancellationToken);

        if (!updated)
        {
            return Error.NotFound(
                "ServiceDocumentation.NotFound",
                $"Service documentation '{command.ServiceDocumentationId}' was not found.");
        }

        return Result.Success();
    }
}