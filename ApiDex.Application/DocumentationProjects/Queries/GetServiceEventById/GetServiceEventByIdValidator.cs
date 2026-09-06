using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceEventById;

public class GetServiceEventByIdValidator : AbstractValidator<GetServiceEventByIdRequest>
{
    public GetServiceEventByIdValidator()
    {
        RuleFor(request => request.ServiceEventId).NotEmpty();
    }
}