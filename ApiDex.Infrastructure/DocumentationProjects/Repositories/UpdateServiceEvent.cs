using ApiDex.Domain.DocumentationProjects.Enums;
using ApiDex.Infrastructure.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<bool> UpdateServiceEventAsync(Guid serviceEventId, IReadOnlyList<Guid> serviceEndpointIds,
        string name, string description,
        EServiceEventDirection direction, CancellationToken cancellationToken = default)
    {
        var serviceEvent = await context.ServiceEvents
            .Include(entity => entity.EventEndpoints)
            .FirstOrDefaultAsync(entity => entity.Id == serviceEventId, cancellationToken);

        if (serviceEvent is null) return false;

        var distinctEndpointIds = serviceEndpointIds.Distinct().ToList();
        var endpointCount = await context.ServiceEndpoints
            .CountAsync(endpoint =>
                    endpoint.ServiceDocumentationId == serviceEvent.ServiceDocumentationId &&
                    distinctEndpointIds.Contains(endpoint.Id),
                cancellationToken);

        if (endpointCount != distinctEndpointIds.Count) return false;

        serviceEvent.Name = name;
        serviceEvent.Description = description;
        serviceEvent.Direction = direction;

        var selectedEndpointIds = distinctEndpointIds.ToHashSet();
        var existingLinks = serviceEvent.EventEndpoints.ToList();

        foreach (var link in existingLinks.Where(link => !selectedEndpointIds.Contains(link.ServiceEndpointId)))
        {
            context.ServiceEventEndpoints.Remove(link);
        }

        var existingEndpointIds = existingLinks.Select(link => link.ServiceEndpointId).ToHashSet();
        foreach (var endpointId in selectedEndpointIds.Where(endpointId => !existingEndpointIds.Contains(endpointId)))
        {
            serviceEvent.EventEndpoints.Add(new ServiceEventEndpointEntity
            {
                ServiceEventId = serviceEvent.Id,
                ServiceEndpointId = endpointId
            });
        }

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
