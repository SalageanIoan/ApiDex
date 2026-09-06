using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceEndpointsByProjectId;

public sealed class GetServiceEndpointsByProjectIdValidator : AbstractValidator<GetServiceEndpointsByProjectIdRequest>
{
    public GetServiceEndpointsByProjectIdValidator()
    {
        RuleFor(request => request.DocumentationProjectId).NotEmpty();
    }
}
