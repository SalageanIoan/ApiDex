using ApiDex.Infrastructure.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiDex.Infrastructure.DocumentationProjects.Configurations;

public class ServiceEventConfiguration : IEntityTypeConfiguration<ServiceEventEntity>
{
    public void Configure(EntityTypeBuilder<ServiceEventEntity> builder)
    {
        builder.ToTable("ServiceEvents");

        builder.HasKey(serviceEvent => serviceEvent.Id);

        builder.Property(serviceEvent => serviceEvent.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(serviceEvent => serviceEvent.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(serviceEvent => serviceEvent.Direction)
            .IsRequired();

        builder.HasIndex(serviceEvent => serviceEvent.ServiceDocumentationId);
    }
}