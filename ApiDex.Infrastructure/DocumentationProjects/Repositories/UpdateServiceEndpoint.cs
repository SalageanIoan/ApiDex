using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<bool> UpdateServiceEndpointAsync(Guid serviceEndpointId,
        string httpMethod, string route, string description, CancellationToken cancellationToken = default)
    {
        var endpoint = await context.ServiceEndpoints
            .FirstOrDefaultAsync(entity => entity.Id == serviceEndpointId, cancellationToken);

        if (endpoint is null) return false;

        endpoint.HttpMethod = httpMethod;
        endpoint.Route = route;
        endpoint.Description = description;

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}