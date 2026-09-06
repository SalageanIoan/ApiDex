using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceEndpointsByProjectId;

public class GetServiceEndpointsByProjectIdHandler(IDocumentationProjectRepository documentationProjectRepository)
    : IRequestHandler<GetServiceEndpointsByProjectIdRequest, Result<List<ServiceEndpointOut>>>
{
    public async Task<Result<List<ServiceEndpointOut>>> Handle(
        GetServiceEndpointsByProjectIdRequest request,
        CancellationToken cancellationToken)
    {
        var endpoints = await documentationProjectRepository.GetServiceEndpointsByProjectIdAsync(
            request.DocumentationProjectId,
            cancellationToken);

        var result = endpoints.Select(endpoint => new ServiceEndpointOut
        {
            Id = endpoint.Id,
            ServiceDocumentationId = endpoint.ServiceDocumentationId,
            ServiceTitle = endpoint.ServiceTitle,
            Key = endpoint.Key,
            HttpMethod = endpoint.HttpMethod,
            Route = endpoint.Route,
            Description = endpoint.Description
        }).ToList();

        return result;
    }
}
