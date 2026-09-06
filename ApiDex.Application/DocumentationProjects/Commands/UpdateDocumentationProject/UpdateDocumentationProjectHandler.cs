using ApiDex.Domain.DocumentationProjects;
using ApiDex.Application.Mediator.Commands;
using ApiDex.Domain.Results;

namespace ApiDex.Application.DocumentationProjects.Commands.UpdateDocumentationProject;

public class UpdateDocumentationProjectHandler(IDocumentationProjectRepository documentationProjectRepository)
    : CommandHandler<UpdateDocumentationProjectRequest>
{
    public override async Task<Result> Handle(UpdateDocumentationProjectRequest command,
        CancellationToken cancellationToken)
    {
        var message = command.Message;

        var updated = await documentationProjectRepository.UpdateDocumentationProjectAsync(
            command.DocumentationProjectId,
            message.Title.Trim(),
            message.GeneralDescription.Trim(),
            message.ArchitectureType,
            cancellationToken);

        if (!updated)
        {
            return Error.NotFound(
                "DocumentationProject.NotFound",
                $"Documentation project '{command.DocumentationProjectId}' was not found.");
        }

        return Result.Success();
    }
}