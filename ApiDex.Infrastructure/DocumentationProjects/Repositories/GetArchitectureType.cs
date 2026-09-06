using ApiDex.Domain.DocumentationProjects.Enums;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<ESystemArchitecture?> GetArchitectureTypeAsync(Guid documentationProjectId,
        CancellationToken cancellationToken = default)
    {
        var project = await context.DocumentationProjects
            .AsNoTracking()
            .Where(p => p.Id == documentationProjectId)
            .Select(p => new { p.ArchitectureType })
            .FirstOrDefaultAsync(cancellationToken);

        return project?.ArchitectureType;
    }

    public async Task<ESystemArchitecture?> GetArchitectureTypeByServiceIdAsync(Guid serviceDocumentationId,
        CancellationToken cancellationToken = default)
    {
        var architectureType = await context.ServiceDocumentations
            .AsNoTracking()
            .Where(s => s.Id == serviceDocumentationId)
            .Select(s => (ESystemArchitecture?)s.DocumentationProject.ArchitectureType)
            .FirstOrDefaultAsync(cancellationToken);

        return architectureType;
    }
}
