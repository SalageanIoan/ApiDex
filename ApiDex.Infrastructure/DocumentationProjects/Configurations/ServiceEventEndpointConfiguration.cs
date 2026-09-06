using ApiDex.Infrastructure.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiDex.Infrastructure.DocumentationProjects.Configurations;

public class ServiceEventEndpointConfiguration : IEntityTypeConfiguration<ServiceEventEndpointEntity>
{
    public void Configure(EntityTypeBuilder<ServiceEventEndpointEntity> builder)
    {
        builder.ToTable("ServiceEventEndpoints");

        builder.HasKey(link => new { link.ServiceEventId, link.ServiceEndpointId });

        builder.HasOne(link => link.ServiceEvent)
            .WithMany(serviceEvent => serviceEvent.EventEndpoints)
            .HasForeignKey(link => link.ServiceEventId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(link => link.ServiceEndpoint)
            .WithMany(endpoint => endpoint.EventEndpoints)
            .HasForeignKey(link => link.ServiceEndpointId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(link => link.ServiceEndpointId);
    }
}
