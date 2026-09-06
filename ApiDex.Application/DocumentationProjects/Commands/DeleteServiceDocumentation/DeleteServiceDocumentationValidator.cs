using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Commands.DeleteServiceDocumentation;

public class DeleteServiceDocumentationValidator : AbstractValidator<DeleteServiceDocumentationRequest>
{
    public DeleteServiceDocumentationValidator()
    {
        RuleFor(request => request.ServiceDocumentationId).NotEmpty();
    }
}