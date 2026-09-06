using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Commands.CreateServiceEvent;

public class CreateServiceEventValidator : AbstractValidator<CreateServiceEventRequest>
{
    public CreateServiceEventValidator()
    {
        RuleFor(request => request.Message.ServiceDocumentationId).NotEmpty();
        RuleFor(request => request.Message.ServiceEndpointIds).NotEmpty();
        RuleForEach(request => request.Message.ServiceEndpointIds).NotEmpty();
        RuleFor(request => request.Message.Name).NotEmpty();
        RuleFor(request => request.Message.Description).NotEmpty();
        RuleFor(request => request.Message.Direction).IsInEnum();
    }
}
