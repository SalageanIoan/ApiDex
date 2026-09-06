using ApiDex.Domain.DocumentationProjects.Enums;
using ApiDex.Infrastructure.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task CreateServiceEventAsync(Guid serviceDocumentationId, IReadOnlyList<Guid> serviceEndpointIds,
        string name, string description, EServiceEventDirection direction,
        CancellationToken cancellationToken = default)
    {
        var serviceExists = await context.ServiceDocumentations
            .AnyAsync(service => service.Id == serviceDocumentationId, cancellationToken);

        if (!serviceExists)
            throw new InvalidOperationException($"Service documentation with id '{serviceDocumentationId}' was not found.");

        var distinctEndpointIds = serviceEndpointIds.Distinct().ToList();
        if (distinctEndpointIds.Count == 0)
            throw new InvalidOperationException("At least one endpoint must be selected for the event.");

        var endpointCount = await context.ServiceEndpoints
            .CountAsync(endpoint =>
                    endpoint.ServiceDocumentationId == serviceDocumentationId &&
                    distinctEndpointIds.Contains(endpoint.Id),
                cancellationToken);

        if (endpointCount != distinctEndpointIds.Count)
            throw new InvalidOperationException("One or more selected endpoints were not found in the service.");

        var serviceEventEntity = new ServiceEventEntity
        {
            Id = Guid.NewGuid(),
            ServiceDocumentationId = serviceDocumentationId,
            Name = name,
            Description = description,
            Direction = direction,
            EventEndpoints = distinctEndpointIds.Select(endpointId => new ServiceEventEndpointEntity
            {
                ServiceEndpointId = endpointId
            }).ToList()
        };

        context.ServiceEvents.Add(serviceEventEntity);
        await context.SaveChangesAsync(cancellationToken);
    }
}
