using ApiDex.Domain.DocumentationProjects;
using ApiDex.Infrastructure.DocumentationProjects.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<ServiceDependency?> GetServiceDependencyByIdAsync(Guid serviceDependencyId,
        CancellationToken cancellationToken = default)
    {
        var dependency = await context.ServiceDependencies
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Id == serviceDependencyId, cancellationToken);

        return dependency is null ? null : DocumentationProjectMappings.MapToServiceDependency(dependency);
    }
}