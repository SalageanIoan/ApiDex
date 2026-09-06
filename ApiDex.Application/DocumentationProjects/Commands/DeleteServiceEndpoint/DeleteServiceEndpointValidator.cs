using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Commands.DeleteServiceEndpoint;

public class DeleteServiceEndpointValidator : AbstractValidator<DeleteServiceEndpointRequest>
{
    public DeleteServiceEndpointValidator()
    {
        RuleFor(request => request.ServiceEndpointId).NotEmpty();
    }
}