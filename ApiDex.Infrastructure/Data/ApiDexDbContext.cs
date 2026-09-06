using ApiDex.Infrastructure.Data.Entity;
using ApiDex.Infrastructure.DocumentationProjects.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ApiDex.Infrastructure.Data;

public class ApiDexDbContext(DbContextOptions<ApiDexDbContext> options) : DbContext(options)
{
    public DbSet<DocumentationProjectEntity> DocumentationProjects { get; set; }

    public DbSet<ServiceDocumentationEntity> ServiceDocumentations { get; set; }

    public DbSet<ServiceEndpointEntity> ServiceEndpoints { get; set; }

    public DbSet<ServiceEventEntity> ServiceEvents { get; set; }

    public DbSet<ServiceEventEndpointEntity> ServiceEventEndpoints { get; set; }

    public DbSet<ServiceDependencyEntity> ServiceDependencies { get; set; }

    public DbSet<RagSettingsEntity> RagSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ApiDex");

        modelBuilder.ApplyConfiguration(new DocumentationProjectConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceDocumentationConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceEndpointConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceEventConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceEventEndpointConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceDependencyConfiguration());
        modelBuilder.ApplyConfiguration(new RagSettingsConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
