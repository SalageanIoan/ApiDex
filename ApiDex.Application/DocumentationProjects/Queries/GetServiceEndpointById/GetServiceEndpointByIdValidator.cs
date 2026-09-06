using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceEndpointById;

public class GetServiceEndpointByIdValidator : AbstractValidator<GetServiceEndpointByIdRequest>
{
    public GetServiceEndpointByIdValidator()
    {
        RuleFor(request => request.ServiceEndpointId).NotEmpty();
    }
}