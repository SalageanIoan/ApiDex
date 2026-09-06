using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<bool> DeleteServiceEventAsync(Guid serviceEventId,
        CancellationToken cancellationToken = default)
    {
        var serviceEvent = await context.ServiceEvents
            .FirstOrDefaultAsync(entity => entity.Id == serviceEventId, cancellationToken);

        if (serviceEvent is null) return false;

        context.ServiceEvents.Remove(serviceEvent);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}