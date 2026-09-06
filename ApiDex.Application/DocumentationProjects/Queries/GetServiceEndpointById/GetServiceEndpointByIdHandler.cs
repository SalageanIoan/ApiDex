using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceEndpointById;

public class GetServiceEndpointByIdHandler(IDocumentationProjectRepository documentationProjectRepository)
    : IRequestHandler<GetServiceEndpointByIdRequest, Result<ServiceEndpointOut>>
{
    public async Task<Result<ServiceEndpointOut>> Handle(GetServiceEndpointByIdRequest request,
        CancellationToken cancellationToken)
    {
        var endpoint = await documentationProjectRepository.GetServiceEndpointByIdAsync(
            request.ServiceEndpointId,
            cancellationToken);

        if (endpoint is null)
        {
            return Error.NotFound(
                "ServiceEndpoint.NotFound",
                $"Service endpoint '{request.ServiceEndpointId}' was not found.");
        }

        return new ServiceEndpointOut
        {
            Id = endpoint.Id,
            ServiceDocumentationId = endpoint.ServiceDocumentationId,
            ServiceTitle = endpoint.ServiceTitle,
            Key = endpoint.Key,
            HttpMethod = endpoint.HttpMethod,
            Route = endpoint.Route,
            Description = endpoint.Description
        };
    }
}