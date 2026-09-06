using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<bool> DeleteDocumentationProjectAsync(Guid documentationProjectId,
        CancellationToken cancellationToken = default)
    {
        var project = await context.DocumentationProjects
            .FirstOrDefaultAsync(entity => entity.Id == documentationProjectId, cancellationToken);

        if (project is null) return false;

        context.DocumentationProjects.Remove(project);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}