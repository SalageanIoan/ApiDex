using ApiDex.Infrastructure.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiDex.Infrastructure.DocumentationProjects.Configurations;

public class ServiceEndpointConfiguration : IEntityTypeConfiguration<ServiceEndpointEntity>
{
    public void Configure(EntityTypeBuilder<ServiceEndpointEntity> builder)
    {
        builder.ToTable("ServiceEndpoints");

        builder.HasKey(endpoint => endpoint.Id);

        builder.Property(endpoint => endpoint.Key)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(endpoint => endpoint.HttpMethod)
            .IsRequired()
            .HasMaxLength(16);

        builder.Property(endpoint => endpoint.Route)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(endpoint => endpoint.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.HasIndex(endpoint => new { endpoint.ServiceDocumentationId, endpoint.Key })
            .IsUnique();
    }
}