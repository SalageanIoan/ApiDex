using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<bool> DeleteServiceDependencyAsync(Guid serviceDependencyId,
        CancellationToken cancellationToken = default)
    {
        var dependency = await context.ServiceDependencies
            .FirstOrDefaultAsync(entity => entity.Id == serviceDependencyId, cancellationToken);

        if (dependency is null) return false;

        context.ServiceDependencies.Remove(dependency);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}