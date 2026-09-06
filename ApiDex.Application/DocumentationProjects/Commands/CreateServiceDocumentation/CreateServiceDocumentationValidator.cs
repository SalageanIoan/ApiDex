using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Commands.CreateServiceDocumentation;

public class CreateServiceDocumentationValidator : AbstractValidator<CreateServiceDocumentationRequest>
{
    public CreateServiceDocumentationValidator()
    {
        RuleFor(request => request.Message.DocumentationProjectId).NotEmpty();
        RuleFor(request => request.Message.Title).NotEmpty();
        RuleFor(request => request.Message.GeneralDescription).NotEmpty();
    }
}