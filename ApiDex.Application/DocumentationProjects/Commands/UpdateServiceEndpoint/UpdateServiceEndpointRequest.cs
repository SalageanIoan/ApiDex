using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Commands.UpdateServiceEndpoint;

public sealed record UpdateServiceEndpointRequest(
    Guid ServiceEndpointId,
    UpdateServiceEndpointIn Message) : IRequest<Result>
{
    public static UpdateServiceEndpointRequest FromMessage(Guid serviceEndpointId,
        UpdateServiceEndpointIn message)
    {
        return new UpdateServiceEndpointRequest(serviceEndpointId, message);
    }
}
