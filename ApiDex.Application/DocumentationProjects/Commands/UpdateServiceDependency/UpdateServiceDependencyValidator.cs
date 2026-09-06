using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Commands.UpdateServiceDependency;

public class UpdateServiceDependencyValidator : AbstractValidator<UpdateServiceDependencyRequest>
{
    public UpdateServiceDependencyValidator()
    {
        RuleFor(request => request.ServiceDependencyId).NotEmpty();
        RuleFor(request => request.Message.SourceEndpointId).NotEmpty();
        RuleFor(request => request.Message.TargetEndpointId)
            .Must(id => id is null || id.Value != Guid.Empty);
        RuleFor(request => request.Message.DependencyType).IsInEnum();
        RuleFor(request => request.Message.Description).NotEmpty();
    }
}
