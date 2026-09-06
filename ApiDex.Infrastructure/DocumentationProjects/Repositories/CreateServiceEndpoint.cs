using ApiDex.Infrastructure.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task CreateServiceEndpointAsync(Guid serviceDocumentationId, string key,
        string httpMethod, string route, string description,
        CancellationToken cancellationToken = default)
    {
        var serviceExists = await context.ServiceDocumentations
            .AnyAsync(service => service.Id == serviceDocumentationId, cancellationToken);

        if (!serviceExists)
            throw new InvalidOperationException($"Service documentation with id '{serviceDocumentationId}' was not found.");

        var endpointEntity = new ServiceEndpointEntity
        {
            Id = Guid.NewGuid(),
            ServiceDocumentationId = serviceDocumentationId,
            Key = key,
            HttpMethod = httpMethod,
            Route = route,
            Description = description
        };

        context.ServiceEndpoints.Add(endpointEntity);
        await context.SaveChangesAsync(cancellationToken);
    }
}