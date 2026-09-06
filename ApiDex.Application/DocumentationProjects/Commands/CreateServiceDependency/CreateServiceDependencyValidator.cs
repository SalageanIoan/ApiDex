using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Commands.CreateServiceDependency;

public class CreateServiceDependencyValidator : AbstractValidator<CreateServiceDependencyRequest>
{
    public CreateServiceDependencyValidator()
    {
        RuleFor(request => request.Message.ServiceDocumentationId).NotEmpty();
        RuleFor(request => request.Message.SourceEndpointId).NotEmpty();
        RuleFor(request => request.Message.TargetEndpointId)
            .Must(id => id is null || id.Value != Guid.Empty);
        RuleFor(request => request.Message.DependencyType).IsInEnum();
        RuleFor(request => request.Message.Description).NotEmpty();
    }
}
