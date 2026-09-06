using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Commands.DeleteServiceDependency;

public class DeleteServiceDependencyValidator : AbstractValidator<DeleteServiceDependencyRequest>
{
    public DeleteServiceDependencyValidator()
    {
        RuleFor(request => request.ServiceDependencyId).NotEmpty();
    }
}