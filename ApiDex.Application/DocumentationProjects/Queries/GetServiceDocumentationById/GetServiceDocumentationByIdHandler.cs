using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceDocumentationById;

public class GetServiceDocumentationByIdHandler(IDocumentationProjectRepository documentationProjectRepository)
    : IRequestHandler<GetServiceDocumentationByIdRequest, Result<ServiceDocumentationOut>>
{
    public async Task<Result<ServiceDocumentationOut>> Handle(GetServiceDocumentationByIdRequest request,
        CancellationToken cancellationToken)
    {
        var service = await documentationProjectRepository.GetServiceDocumentationByIdAsync(
            request.ServiceDocumentationId,
            cancellationToken);

        if (service is null)
        {
            return Error.NotFound(
                "ServiceDocumentation.NotFound",
                $"Service documentation '{request.ServiceDocumentationId}' was not found.");
        }

        return new ServiceDocumentationOut
        {
            Id = service.Id,
            Title = service.Title,
            GeneralDescription = service.GeneralDescription,
            Endpoints = service.Endpoints.Select(e => new ServiceEndpointOut
            {
                Id = e.Id,
                Key = e.Key,
                HttpMethod = e.HttpMethod,
                Route = e.Route,
                Description = e.Description
            }).ToList(),
            Events = service.Events.Select(e => new ServiceEventOut
            {
                Id = e.Id,
                ServiceEndpointIds = e.ServiceEndpointIds.ToList(),
                Name = e.Name,
                Description = e.Description,
                Direction = e.Direction
            }).ToList(),
            Dependencies = service.Dependencies.Select(d => new ServiceDependencyOut
            {
                Id = d.Id,
                SourceEndpointId = d.SourceEndpointId,
                DependencyType = d.DependencyType,
                TargetEndpointId = d.TargetEndpointId,
                TargetServiceTitle = d.TargetServiceTitle,
                TargetEndpointRoute = d.TargetEndpointRoute,
                Description = d.Description
            }).ToList()
        };
    }
}
