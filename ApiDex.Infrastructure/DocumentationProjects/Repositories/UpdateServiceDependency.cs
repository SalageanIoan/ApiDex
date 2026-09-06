using ApiDex.Domain.DocumentationProjects.Enums;
using ApiDex.Infrastructure.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<bool> UpdateServiceDependencyAsync(Guid serviceDependencyId, Guid sourceEndpointId,
        EServiceDependencyType dependencyType, Guid? targetEndpointId, string? targetServiceTitle,
        string? targetEndpointRoute, string description, CancellationToken cancellationToken = default)
    {
        var dependency = await context.ServiceDependencies
            .FirstOrDefaultAsync(entity => entity.Id == serviceDependencyId, cancellationToken);

        if (dependency is null) return false;

        ServiceEndpointEntity? targetEndpoint = null;
        if (targetEndpointId.HasValue)
        {
            targetEndpoint = await context.ServiceEndpoints
                .Include(endpoint => endpoint.ServiceDocumentation)
                .FirstOrDefaultAsync(endpoint => endpoint.Id == targetEndpointId.Value, cancellationToken);

            if (targetEndpoint is null) return false;
        }

        var resolvedTargetServiceTitle = targetEndpoint?.ServiceDocumentation.Title ?? targetServiceTitle;
        var resolvedTargetEndpointRoute = targetEndpoint?.Route ?? targetEndpointRoute;

        dependency.SourceEndpointId = sourceEndpointId;
        dependency.DependencyType = dependencyType;
        dependency.TargetEndpointId = targetEndpointId;
        dependency.TargetServiceTitle = resolvedTargetServiceTitle;
        dependency.TargetEndpointRoute = resolvedTargetEndpointRoute;
        dependency.Description = description;

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
