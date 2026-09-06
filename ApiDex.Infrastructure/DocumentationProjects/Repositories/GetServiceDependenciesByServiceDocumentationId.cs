using ApiDex.Domain.DocumentationProjects;
using ApiDex.Infrastructure.DocumentationProjects.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<List<ServiceDependency>> GetServiceDependenciesByServiceDocumentationIdAsync(
        Guid serviceDocumentationId, CancellationToken cancellationToken = default)
    {
        var dependencies = await context.ServiceDependencies
            .AsNoTracking()
            .Where(entity => entity.ServiceDocumentationId == serviceDocumentationId)
            .ToListAsync(cancellationToken);

        return dependencies.Select(DocumentationProjectMappings.MapToServiceDependency).ToList();
    }
}