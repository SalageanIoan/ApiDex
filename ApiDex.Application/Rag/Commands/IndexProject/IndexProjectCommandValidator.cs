using FluentValidation;

namespace ApiDex.Application.Rag.Commands.IndexProject;

public sealed class IndexProjectCommandValidator : AbstractValidator<IndexProjectCommand>
{
    public IndexProjectCommandValidator()
    {
        RuleFor(request => request.ProjectId).NotEmpty();
    }
}
