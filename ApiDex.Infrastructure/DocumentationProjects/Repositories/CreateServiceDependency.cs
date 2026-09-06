using ApiDex.Domain.DocumentationProjects.Enums;
using ApiDex.Infrastructure.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task CreateServiceDependencyAsync(Guid serviceDocumentationId,
        Guid sourceEndpointId, EServiceDependencyType dependencyType,
        Guid? targetEndpointId, string? targetServiceTitle, string? targetEndpointRoute,
        string description,
        CancellationToken cancellationToken = default)
    {
        var serviceExists = await context.ServiceDocumentations
            .AnyAsync(service => service.Id == serviceDocumentationId, cancellationToken);

        if (!serviceExists)
            throw new InvalidOperationException($"Service documentation with id '{serviceDocumentationId}' was not found.");

        var sourceEndpointExists = await context.ServiceEndpoints
            .AnyAsync(endpoint => endpoint.Id == sourceEndpointId && endpoint.ServiceDocumentationId == serviceDocumentationId,
                cancellationToken);

        if (!sourceEndpointExists)
            throw new InvalidOperationException($"Source endpoint with id '{sourceEndpointId}' was not found in service '{serviceDocumentationId}'.");

        ServiceEndpointEntity? targetEndpoint = null;
        if (targetEndpointId.HasValue)
        {
            targetEndpoint = await context.ServiceEndpoints
                .Include(endpoint => endpoint.ServiceDocumentation)
                .FirstOrDefaultAsync(endpoint => endpoint.Id == targetEndpointId.Value, cancellationToken);

            if (targetEndpoint is null)
                throw new InvalidOperationException($"Target endpoint with id '{targetEndpointId}' was not found.");
        }

        var resolvedTargetServiceTitle = targetEndpoint?.ServiceDocumentation.Title ?? targetServiceTitle;
        var resolvedTargetEndpointRoute = targetEndpoint?.Route ?? targetEndpointRoute;

        var dependencyEntity = new ServiceDependencyEntity
        {
            Id = Guid.NewGuid(),
            ServiceDocumentationId = serviceDocumentationId,
            SourceEndpointId = sourceEndpointId,
            DependencyType = dependencyType,
            TargetEndpointId = targetEndpointId,
            TargetServiceTitle = resolvedTargetServiceTitle,
            TargetEndpointRoute = resolvedTargetEndpointRoute,
            Description = description
        };

        context.ServiceDependencies.Add(dependencyEntity);
        await context.SaveChangesAsync(cancellationToken);
    }
}
