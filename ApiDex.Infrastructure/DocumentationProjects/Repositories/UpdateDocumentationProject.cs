using ApiDex.Domain.DocumentationProjects.Enums;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<bool> UpdateDocumentationProjectAsync(Guid documentationProjectId, string title,
        string generalDescription, ESystemArchitecture architectureType,
        CancellationToken cancellationToken = default)
    {
        var project = await context.DocumentationProjects
            .FirstOrDefaultAsync(entity => entity.Id == documentationProjectId, cancellationToken);

        if (project is null) return false;

        project.Title = title;
        project.GeneralDescription = generalDescription;
        project.ArchitectureType = architectureType;

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}