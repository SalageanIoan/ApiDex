using ApiDex.Domain.DocumentationProjects;
using ApiDex.Infrastructure.DocumentationProjects.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<DocumentationProject?> GetProjectDataForIndexingAsync(Guid documentationProjectId,
        CancellationToken cancellationToken = default)
    {
        var project = await context.DocumentationProjects
            .AsNoTracking()
            .Include(p => p.Services)
                .ThenInclude(s => s.Endpoints)
            .Include(p => p.Services)
                .ThenInclude(s => s.Events)
                    .ThenInclude(e => e.EventEndpoints)
            .Include(p => p.Services)
                .ThenInclude(s => s.Dependencies)
            .FirstOrDefaultAsync(p => p.Id == documentationProjectId, cancellationToken);

        return project is null ? null : DocumentationProjectMappings.MapToDocumentationProject(project);
    }
}
