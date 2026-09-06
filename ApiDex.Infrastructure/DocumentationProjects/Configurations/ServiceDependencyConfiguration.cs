using ApiDex.Infrastructure.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiDex.Infrastructure.DocumentationProjects.Configurations;

public class ServiceDependencyConfiguration : IEntityTypeConfiguration<ServiceDependencyEntity>
{
    public void Configure(EntityTypeBuilder<ServiceDependencyEntity> builder)
    {
        builder.ToTable("ServiceDependencies");

        builder.HasKey(dependency => dependency.Id);

        builder.Property(dependency => dependency.DependencyType)
            .IsRequired();

        builder.Property(dependency => dependency.TargetServiceTitle)
            .HasMaxLength(200);

        builder.Property(dependency => dependency.TargetEndpointRoute)
            .HasMaxLength(500);

        builder.Property(dependency => dependency.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.HasOne(dependency => dependency.SourceEndpoint)
            .WithMany(endpoint => endpoint.SourceDependencies)
            .HasForeignKey(dependency => dependency.SourceEndpointId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(dependency => dependency.TargetEndpoint)
            .WithMany(endpoint => endpoint.TargetDependencies)
            .HasForeignKey(dependency => dependency.TargetEndpointId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(dependency => dependency.ServiceDocumentationId);
        builder.HasIndex(dependency => dependency.SourceEndpointId);
        builder.HasIndex(dependency => dependency.TargetEndpointId);
    }
}