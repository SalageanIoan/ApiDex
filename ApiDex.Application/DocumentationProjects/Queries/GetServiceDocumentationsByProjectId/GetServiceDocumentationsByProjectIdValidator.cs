using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceDocumentationsByProjectId;

public class GetServiceDocumentationsByProjectIdValidator
    : AbstractValidator<GetServiceDocumentationsByProjectIdRequest>
{
    public GetServiceDocumentationsByProjectIdValidator()
    {
        RuleFor(request => request.DocumentationProjectId).NotEmpty();
    }
}