using ApiDex.Domain.DocumentationProjects;
using ApiDex.Infrastructure.DocumentationProjects.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<ServiceEndpoint?> GetServiceEndpointByIdAsync(Guid serviceEndpointId,
        CancellationToken cancellationToken = default)
    {
        var endpoint = await context.ServiceEndpoints
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Id == serviceEndpointId, cancellationToken);

        return endpoint is null ? null : DocumentationProjectMappings.MapToServiceEndpoint(endpoint);
    }
}