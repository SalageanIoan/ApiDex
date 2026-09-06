using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceEndpointById;

public sealed record GetServiceEndpointByIdRequest(Guid ServiceEndpointId)
    : IRequest<Result<ServiceEndpointOut>>
{
    public static GetServiceEndpointByIdRequest FromId(Guid serviceEndpointId)
    {
        return new GetServiceEndpointByIdRequest(serviceEndpointId);
    }
}
