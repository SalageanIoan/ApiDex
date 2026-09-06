using FluentValidation;

namespace ApiDex.Application.Rag.Queries.AskDocumentation;

public sealed class AskDocumentationQueryValidator : AbstractValidator<AskDocumentationQuery>
{
    public AskDocumentationQueryValidator()
    {
        RuleFor(request => request.Question).NotEmpty();
        RuleFor(request => request.ProjectId)
            .Must(id => id is null || id.Value != Guid.Empty);
        RuleFor(request => request.ServiceId)
            .Must(id => id is null || id.Value != Guid.Empty);
    }
}
