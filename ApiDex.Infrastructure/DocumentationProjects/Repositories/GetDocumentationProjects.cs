using ApiDex.Domain.DocumentationProjects;
using ApiDex.Infrastructure.DocumentationProjects.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<List<DocumentationProject>> GetDocumentationProjectsAsync(
        CancellationToken cancellationToken = default)
    {
        var projects = await context.DocumentationProjects
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return projects.Select(DocumentationProjectMappings.MapToDocumentationProject).ToList();
    }
}