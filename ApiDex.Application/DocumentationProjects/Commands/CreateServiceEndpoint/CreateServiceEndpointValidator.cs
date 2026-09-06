using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Commands.CreateServiceEndpoint;

public class CreateServiceEndpointValidator : AbstractValidator<CreateServiceEndpointRequest>
{
    public CreateServiceEndpointValidator()
    {
        RuleFor(request => request.Message.ServiceDocumentationId).NotEmpty();
        RuleFor(request => request.Message.HttpMethod).NotEmpty();
        RuleFor(request => request.Message.Route).NotEmpty();
        RuleFor(request => request.Message.Description).NotEmpty();
    }
}
