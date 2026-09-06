using ApiDex.Domain.DocumentationProjects;
using ApiDex.Infrastructure.DocumentationProjects.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<List<ServiceEvent>> GetServiceEventsByServiceDocumentationIdAsync(
        Guid serviceDocumentationId, CancellationToken cancellationToken = default)
    {
        var events = await context.ServiceEvents
            .AsNoTracking()
            .Include(entity => entity.EventEndpoints)
            .Where(entity => entity.ServiceDocumentationId == serviceDocumentationId)
            .ToListAsync(cancellationToken);

        return events.Select(DocumentationProjectMappings.MapToServiceEvent).ToList();
    }
}
