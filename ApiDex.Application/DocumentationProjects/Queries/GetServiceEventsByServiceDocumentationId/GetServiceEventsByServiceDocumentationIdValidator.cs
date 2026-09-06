using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceEventsByServiceDocumentationId;

public class GetServiceEventsByServiceDocumentationIdValidator
    : AbstractValidator<GetServiceEventsByServiceDocumentationIdRequest>
{
    public GetServiceEventsByServiceDocumentationIdValidator()
    {
        RuleFor(request => request.ServiceDocumentationId).NotEmpty();
    }
}