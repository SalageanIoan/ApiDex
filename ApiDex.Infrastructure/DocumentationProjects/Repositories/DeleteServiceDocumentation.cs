using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<bool> DeleteServiceDocumentationAsync(Guid serviceDocumentationId,
        CancellationToken cancellationToken = default)
    {
        var service = await context.ServiceDocumentations
            .FirstOrDefaultAsync(entity => entity.Id == serviceDocumentationId, cancellationToken);

        if (service is null) return false;

        context.ServiceDocumentations.Remove(service);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}