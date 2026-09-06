using FluentValidation;

namespace ApiDex.Application.Rag.Queries.GetProjectIndexStatus;

public sealed class GetProjectIndexStatusQueryValidator : AbstractValidator<GetProjectIndexStatusQuery>
{
    public GetProjectIndexStatusQueryValidator()
    {
        RuleFor(request => request.ProjectId).NotEmpty();
    }
}
