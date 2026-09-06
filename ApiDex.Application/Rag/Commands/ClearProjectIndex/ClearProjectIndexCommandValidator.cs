using FluentValidation;

namespace ApiDex.Application.Rag.Commands.ClearProjectIndex;

public sealed class ClearProjectIndexCommandValidator : AbstractValidator<ClearProjectIndexCommand>
{
    public ClearProjectIndexCommandValidator()
    {
        RuleFor(request => request.ProjectId).NotEmpty();
    }
}
