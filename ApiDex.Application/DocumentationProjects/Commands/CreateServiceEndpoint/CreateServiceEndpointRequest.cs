using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Commands.CreateServiceEndpoint;

public sealed record CreateServiceEndpointRequest(CreateServiceEndpointIn Message) : IRequest<Result>
{
    public static CreateServiceEndpointRequest FromMessage(CreateServiceEndpointIn message)
    {
        return new CreateServiceEndpointRequest(message);
    }
}
