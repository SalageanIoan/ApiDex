using ApiDex.Infrastructure.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiDex.Infrastructure.DocumentationProjects.Configurations;

public class DocumentationProjectConfiguration : IEntityTypeConfiguration<DocumentationProjectEntity>
{
    public void Configure(EntityTypeBuilder<DocumentationProjectEntity> builder)
    {
        builder.ToTable("DocumentationProjects");

        builder.HasKey(documentationProject => documentationProject.Id);

        builder.Property(documentationProject => documentationProject.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(documentationProject => documentationProject.GeneralDescription)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(documentationProject => documentationProject.ArchitectureType)
            .IsRequired();

        builder.HasMany(documentationProject => documentationProject.Services)
            .WithOne(service => service.DocumentationProject)
            .HasForeignKey(service => service.DocumentationProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(documentationProject => documentationProject.Title);
    }
}