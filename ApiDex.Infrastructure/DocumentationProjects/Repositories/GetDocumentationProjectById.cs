using ApiDex.Domain.DocumentationProjects;
using ApiDex.Infrastructure.DocumentationProjects.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<DocumentationProject?> GetDocumentationProjectByIdAsync(Guid documentationProjectId,
        CancellationToken cancellationToken = default)
    {
        var project = await context.DocumentationProjects
            .AsNoTracking()
            .Include(entity => entity.Services)
            .FirstOrDefaultAsync(entity => entity.Id == documentationProjectId, cancellationToken);

        return project is null ? null : DocumentationProjectMappings.MapToDocumentationProject(project);
    }
}