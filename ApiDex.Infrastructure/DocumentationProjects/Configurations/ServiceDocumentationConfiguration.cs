using ApiDex.Infrastructure.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiDex.Infrastructure.DocumentationProjects.Configurations;

public class ServiceDocumentationConfiguration : IEntityTypeConfiguration<ServiceDocumentationEntity>
{
    public void Configure(EntityTypeBuilder<ServiceDocumentationEntity> builder)
    {
        builder.ToTable("ServiceDocumentations");

        builder.HasKey(serviceDocumentation => serviceDocumentation.Id);

        builder.Property(serviceDocumentation => serviceDocumentation.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(serviceDocumentation => serviceDocumentation.GeneralDescription)
            .IsRequired()
            .HasMaxLength(4000);

        builder.HasMany(serviceDocumentation => serviceDocumentation.Endpoints)
            .WithOne(endpoint => endpoint.ServiceDocumentation)
            .HasForeignKey(endpoint => endpoint.ServiceDocumentationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(serviceDocumentation => serviceDocumentation.Events)
            .WithOne(serviceEvent => serviceEvent.ServiceDocumentation)
            .HasForeignKey(serviceEvent => serviceEvent.ServiceDocumentationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(serviceDocumentation => serviceDocumentation.Dependencies)
            .WithOne(dependency => dependency.ServiceDocumentation)
            .HasForeignKey(dependency => dependency.ServiceDocumentationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(serviceDocumentation => new { serviceDocumentation.DocumentationProjectId, serviceDocumentation.Title })
            .IsUnique();
    }
}