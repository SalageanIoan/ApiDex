using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.DocumentationProjects.Enums;
using ApiDex.Application.Mediator.Commands;
using ApiDex.Domain.Results;

namespace ApiDex.Application.DocumentationProjects.Commands.CreateServiceDocumentation;

public class CreateServiceDocumentationHandler(IDocumentationProjectRepository documentationProjectRepository)
    : CommandHandler<CreateServiceDocumentationRequest>
{
    public override async Task<Result> Handle(CreateServiceDocumentationRequest command,
        CancellationToken cancellationToken)
    {
        var message = command.Message;

        var architectureType = await documentationProjectRepository.GetArchitectureTypeAsync(
            message.DocumentationProjectId, cancellationToken);

        if (architectureType == ESystemArchitecture.Monolith)
        {
            return Error.Validation(
                "ServiceDocumentation.MonolithRestriction",
                "Monolith projects do not support multiple services. Manage endpoints directly on the project.",
                null!);
        }

        await documentationProjectRepository.CreateServiceDocumentationAsync(
            message.DocumentationProjectId,
            message.Title.Trim(),
            message.GeneralDescription.Trim(),
            cancellationToken);

        return Result.Success();
    }
}