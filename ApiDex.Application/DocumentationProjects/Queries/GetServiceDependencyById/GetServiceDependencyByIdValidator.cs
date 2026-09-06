using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceDependencyById;

public class GetServiceDependencyByIdValidator : AbstractValidator<GetServiceDependencyByIdRequest>
{
    public GetServiceDependencyByIdValidator()
    {
        RuleFor(request => request.ServiceDependencyId).NotEmpty();
    }
}