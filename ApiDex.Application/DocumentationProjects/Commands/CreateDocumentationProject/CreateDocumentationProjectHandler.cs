using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.DocumentationProjects.Enums;
using ApiDex.Application.Mediator.Commands;
using ApiDex.Domain.Results;

namespace ApiDex.Application.DocumentationProjects.Commands.CreateDocumentationProject;

public class CreateDocumentationProjectHandler(IDocumentationProjectRepository documentationProjectRepository)
    : CommandHandler<CreateDocumentationProjectRequest>
{
    public override async Task<Result> Handle(CreateDocumentationProjectRequest command, CancellationToken cancellationToken)
    {
        var message = command.Message;

        var projectId = await documentationProjectRepository.CreateDocumentationProjectAsync(
            message.Title.Trim(),
            message.GeneralDescription.Trim(),
            message.ArchitectureType,
            cancellationToken);

        if (message.ArchitectureType == ESystemArchitecture.Monolith)
        {
            await documentationProjectRepository.CreateServiceDocumentationAsync(
                projectId,
                message.Title.Trim(),
                message.GeneralDescription.Trim(),
                cancellationToken);
        }

        return Result.Success();
    }
}