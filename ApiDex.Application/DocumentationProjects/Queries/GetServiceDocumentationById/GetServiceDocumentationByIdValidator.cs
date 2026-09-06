using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceDocumentationById;

public class GetServiceDocumentationByIdValidator : AbstractValidator<GetServiceDocumentationByIdRequest>
{
    public GetServiceDocumentationByIdValidator()
    {
        RuleFor(request => request.ServiceDocumentationId).NotEmpty();
    }
}