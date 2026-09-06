using ApiDex.Domain.DocumentationProjects;
using ApiDex.Infrastructure.DocumentationProjects.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<ServiceEvent?> GetServiceEventByIdAsync(Guid serviceEventId,
        CancellationToken cancellationToken = default)
    {
        var serviceEvent = await context.ServiceEvents
            .AsNoTracking()
            .Include(entity => entity.EventEndpoints)
            .FirstOrDefaultAsync(entity => entity.Id == serviceEventId, cancellationToken);

        return serviceEvent is null ? null : DocumentationProjectMappings.MapToServiceEvent(serviceEvent);
    }
}
