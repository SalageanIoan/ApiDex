using ApiDex.Domain.DocumentationProjects;
using ApiDex.Infrastructure.DocumentationProjects.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<List<ServiceEndpoint>> GetServiceEndpointsByProjectIdAsync(Guid documentationProjectId,
        CancellationToken cancellationToken = default)
    {
        var endpoints = await context.ServiceEndpoints
            .AsNoTracking()
            .Where(e => e.ServiceDocumentation.DocumentationProjectId == documentationProjectId)
            .Include(e => e.ServiceDocumentation)
            .OrderBy(e => e.ServiceDocumentation.Title)
            .ThenBy(e => e.HttpMethod)
            .ThenBy(e => e.Route)
            .ToListAsync(cancellationToken);

        return endpoints.Select(DocumentationProjectMappings.MapToServiceEndpoint).ToList();
    }
}
