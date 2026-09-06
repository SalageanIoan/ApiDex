using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceEndpointsByServiceDocumentationId;

public class GetServiceEndpointsByServiceDocumentationIdValidator
    : AbstractValidator<GetServiceEndpointsByServiceDocumentationIdRequest>
{
    public GetServiceEndpointsByServiceDocumentationIdValidator()
    {
        RuleFor(request => request.ServiceDocumentationId).NotEmpty();
    }
}