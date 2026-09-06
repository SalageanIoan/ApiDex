using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Commands.DeleteServiceEndpoint;

public sealed record DeleteServiceEndpointRequest(Guid ServiceEndpointId) : IRequest<Result>
{
    public static DeleteServiceEndpointRequest FromId(Guid serviceEndpointId)
    {
        return new DeleteServiceEndpointRequest(serviceEndpointId);
    }
}
