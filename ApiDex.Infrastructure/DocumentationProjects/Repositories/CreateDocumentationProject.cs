using ApiDex.Domain.DocumentationProjects.Enums;
using ApiDex.Infrastructure.Data.Entity;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository
{
    public async Task<Guid> CreateDocumentationProjectAsync(string title, string generalDescription,
        ESystemArchitecture architectureType,
        CancellationToken cancellationToken = default)
    {
        var projectEntity = new DocumentationProjectEntity
        {
            Id = Guid.NewGuid(),
            Title = title,
            GeneralDescription = generalDescription,
            ArchitectureType = architectureType
        };

        context.DocumentationProjects.Add(projectEntity);
        await context.SaveChangesAsync(cancellationToken);

        return projectEntity.Id;
    }
}