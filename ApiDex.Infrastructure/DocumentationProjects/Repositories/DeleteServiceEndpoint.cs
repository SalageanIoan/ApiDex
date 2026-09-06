using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<bool> DeleteServiceEndpointAsync(Guid serviceEndpointId,
        CancellationToken cancellationToken = default)
    {
        var endpoint = await context.ServiceEndpoints
            .FirstOrDefaultAsync(entity => entity.Id == serviceEndpointId, cancellationToken);

        if (endpoint is null) return false;

        context.ServiceEndpoints.Remove(endpoint);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}