using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceEndpointsByServiceDocumentationId;

public class GetServiceEndpointsByServiceDocumentationIdHandler
    (IDocumentationProjectRepository documentationProjectRepository)
    : IRequestHandler<GetServiceEndpointsByServiceDocumentationIdRequest, Result<List<ServiceEndpointOut>>>
{
    public async Task<Result<List<ServiceEndpointOut>>> Handle(
        GetServiceEndpointsByServiceDocumentationIdRequest request,
        CancellationToken cancellationToken)
    {
        var endpoints = await documentationProjectRepository.GetServiceEndpointsByServiceDocumentationIdAsync(
            request.ServiceDocumentationId,
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