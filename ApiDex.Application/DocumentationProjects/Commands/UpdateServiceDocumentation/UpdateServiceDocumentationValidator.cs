using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Commands.UpdateServiceDocumentation;

public class UpdateServiceDocumentationValidator : AbstractValidator<UpdateServiceDocumentationRequest>
{
    public UpdateServiceDocumentationValidator()
    {
        RuleFor(request => request.ServiceDocumentationId).NotEmpty();
        RuleFor(request => request.Message.Title).NotEmpty();
        RuleFor(request => request.Message.GeneralDescription).NotEmpty();
    }
}