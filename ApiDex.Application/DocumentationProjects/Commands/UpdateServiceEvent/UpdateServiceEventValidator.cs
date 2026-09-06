using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Commands.UpdateServiceEvent;

public class UpdateServiceEventValidator : AbstractValidator<UpdateServiceEventRequest>
{
    public UpdateServiceEventValidator()
    {
        RuleFor(request => request.ServiceEventId).NotEmpty();
        RuleFor(request => request.Message.ServiceEndpointIds).NotEmpty();
        RuleForEach(request => request.Message.ServiceEndpointIds).NotEmpty();
        RuleFor(request => request.Message.Name).NotEmpty();
        RuleFor(request => request.Message.Description).NotEmpty();
        RuleFor(request => request.Message.Direction).IsInEnum();
    }
}
