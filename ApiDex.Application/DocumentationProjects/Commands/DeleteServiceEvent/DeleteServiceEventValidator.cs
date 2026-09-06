using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Commands.DeleteServiceEvent;

public class DeleteServiceEventValidator : AbstractValidator<DeleteServiceEventRequest>
{
    public DeleteServiceEventValidator()
    {
        RuleFor(request => request.ServiceEventId).NotEmpty();
    }
}