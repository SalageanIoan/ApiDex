using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Commands.UpdateServiceEndpoint;

public class UpdateServiceEndpointValidator : AbstractValidator<UpdateServiceEndpointRequest>
{
    public UpdateServiceEndpointValidator()
    {
        RuleFor(request => request.ServiceEndpointId).NotEmpty();
        RuleFor(request => request.Message.HttpMethod).NotEmpty();
        RuleFor(request => request.Message.Route).NotEmpty();
        RuleFor(request => request.Message.Description).NotEmpty();
    }
}