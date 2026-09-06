using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceEndpointsByProjectId;

public sealed record GetServiceEndpointsByProjectIdRequest(Guid DocumentationProjectId)
    : IRequest<Result<List<ServiceEndpointOut>>>
{
    public static GetServiceEndpointsByProjectIdRequest FromId(Guid documentationProjectId)
    {
        return new GetServiceEndpointsByProjectIdRequest(documentationProjectId);
    }
}
