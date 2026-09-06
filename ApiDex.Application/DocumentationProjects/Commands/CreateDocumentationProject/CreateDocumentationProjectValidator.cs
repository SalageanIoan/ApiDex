using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Commands.CreateDocumentationProject;

public class CreateDocumentationProjectValidator : AbstractValidator<CreateDocumentationProjectRequest>
{
    public CreateDocumentationProjectValidator()
    {
        RuleFor(request => request.Message.Title)
            .NotEmpty();

        RuleFor(request => request.Message.GeneralDescription)
            .NotEmpty();

        RuleFor(request => request.Message.ArchitectureType)
            .IsInEnum();
    }
}