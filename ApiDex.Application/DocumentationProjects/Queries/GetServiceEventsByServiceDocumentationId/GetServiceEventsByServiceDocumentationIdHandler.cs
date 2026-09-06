using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceEventsByServiceDocumentationId;

public class GetServiceEventsByServiceDocumentationIdHandler
    (IDocumentationProjectRepository documentationProjectRepository)
    : IRequestHandler<GetServiceEventsByServiceDocumentationIdRequest, Result<List<ServiceEventOut>>>
{
    public async Task<Result<List<ServiceEventOut>>> Handle(
        GetServiceEventsByServiceDocumentationIdRequest request,
        CancellationToken cancellationToken)
    {
        var events = await documentationProjectRepository.GetServiceEventsByServiceDocumentationIdAsync(
            request.ServiceDocumentationId,
            cancellationToken);

        var result = events.Select(serviceEvent => new ServiceEventOut
        {
            Id = serviceEvent.Id,
            ServiceEndpointIds = serviceEvent.ServiceEndpointIds.ToList(),
            Name = serviceEvent.Name,
            Description = serviceEvent.Description,
            Direction = serviceEvent.Direction
        }).ToList();

        return result;
    }
}
