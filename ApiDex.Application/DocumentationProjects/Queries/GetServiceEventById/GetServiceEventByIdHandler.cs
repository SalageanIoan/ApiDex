using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceEventById;

public class GetServiceEventByIdHandler(IDocumentationProjectRepository documentationProjectRepository)
    : IRequestHandler<GetServiceEventByIdRequest, Result<ServiceEventOut>>
{
    public async Task<Result<ServiceEventOut>> Handle(GetServiceEventByIdRequest request,
        CancellationToken cancellationToken)
    {
        var serviceEvent = await documentationProjectRepository.GetServiceEventByIdAsync(
            request.ServiceEventId,
            cancellationToken);

        if (serviceEvent is null)
        {
            return Error.NotFound(
                "ServiceEvent.NotFound",
                $"Service event '{request.ServiceEventId}' was not found.");
        }

        return new ServiceEventOut
        {
            Id = serviceEvent.Id,
            ServiceEndpointIds = serviceEvent.ServiceEndpointIds.ToList(),
            Name = serviceEvent.Name,
            Description = serviceEvent.Description,
            Direction = serviceEvent.Direction
        };
    }
}
