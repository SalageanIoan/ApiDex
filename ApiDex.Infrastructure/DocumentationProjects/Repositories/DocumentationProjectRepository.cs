using ApiDex.Domain.DocumentationProjects;
using ApiDex.Infrastructure.Data;

namespace ApiDex.Infrastructure.DocumentationProjects.Repositories;

public partial class DocumentationProjectRepository(
    ApiDexDbContext context
) : IDocumentationProjectRepository;