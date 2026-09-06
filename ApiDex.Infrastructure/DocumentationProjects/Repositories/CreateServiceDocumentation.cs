using ApiDex.Infrastructure.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task CreateServiceDocumentationAsync(Guid documentationProjectId, string title,
        string generalDescription,
        CancellationToken cancellationToken = default)
    {
        var documentationProjectExists = await context.DocumentationProjects
            .AnyAsync(project => project.Id == documentationProjectId, cancellationToken);

        if (!documentationProjectExists)
            throw new InvalidOperationException($"Documentation project with id '{documentationProjectId}' was not found.");

        var serviceEntity = new ServiceDocumentationEntity
        {
            Id = Guid.NewGuid(),
            DocumentationProjectId = documentationProjectId,
            Title = title,
            GeneralDescription = generalDescription
        };

        context.ServiceDocumentations.Add(serviceEntity);
        await context.SaveChangesAsync(cancellationToken);
    }
}