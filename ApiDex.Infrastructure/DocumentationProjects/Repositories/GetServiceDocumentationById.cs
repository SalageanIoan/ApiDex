using ApiDex.Domain.DocumentationProjects;
using ApiDex.Infrastructure.DocumentationProjects.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<ServiceDocumentation?> GetServiceDocumentationByIdAsync(Guid serviceDocumentationId,
        CancellationToken cancellationToken = default)
    {
        var service = await context.ServiceDocumentations
            .AsNoTracking()
            .Include(entity => entity.Endpoints)
            .Include(entity => entity.Events)
                .ThenInclude(entity => entity.EventEndpoints)
            .Include(entity => entity.Dependencies)
            .FirstOrDefaultAsync(entity => entity.Id == serviceDocumentationId, cancellationToken);

        return service is null ? null : DocumentationProjectMappings.MapToServiceDocumentation(service);
    }
}
