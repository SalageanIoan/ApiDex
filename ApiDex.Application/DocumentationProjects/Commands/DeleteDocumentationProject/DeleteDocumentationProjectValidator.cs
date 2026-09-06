using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Commands.DeleteDocumentationProject;

public class DeleteDocumentationProjectValidator : AbstractValidator<DeleteDocumentationProjectRequest>
{
    public DeleteDocumentationProjectValidator()
    {
        RuleFor(request => request.DocumentationProjectId).NotEmpty();
    }
}