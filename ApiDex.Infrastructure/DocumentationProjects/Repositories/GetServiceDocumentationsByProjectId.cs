using ApiDex.Domain.DocumentationProjects;
using ApiDex.Infrastructure.DocumentationProjects.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<List<ServiceDocumentation>> GetServiceDocumentationsByProjectIdAsync(
        Guid documentationProjectId, CancellationToken cancellationToken = default)
    {
        var services = await context.ServiceDocumentations
            .AsNoTracking()
            .Where(entity => entity.DocumentationProjectId == documentationProjectId)
            .ToListAsync(cancellationToken);

        return services.Select(DocumentationProjectMappings.MapToServiceDocumentation).ToList();
    }
}