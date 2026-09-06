using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Commands.UpdateDocumentationProject;

public class UpdateDocumentationProjectValidator : AbstractValidator<UpdateDocumentationProjectRequest>
{
    public UpdateDocumentationProjectValidator()
    {
        RuleFor(request => request.DocumentationProjectId).NotEmpty();
        RuleFor(request => request.Message.Title).NotEmpty();
        RuleFor(request => request.Message.GeneralDescription).NotEmpty();
        RuleFor(request => request.Message.ArchitectureType).IsInEnum();
    }
}