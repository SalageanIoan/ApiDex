using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Queries.GetDocumentationProjectById;

public class GetDocumentationProjectByIdValidator : AbstractValidator<GetDocumentationProjectByIdRequest>
{
    public GetDocumentationProjectByIdValidator()
    {
        RuleFor(request => request.DocumentationProjectId).NotEmpty();
    }
}