using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceDependenciesByServiceDocumentationId;

public class GetServiceDependenciesByServiceDocumentationIdValidator
    : AbstractValidator<GetServiceDependenciesByServiceDocumentationIdRequest>
{
    public GetServiceDependenciesByServiceDocumentationIdValidator()
    {
        RuleFor(request => request.ServiceDocumentationId).NotEmpty();
    }
}