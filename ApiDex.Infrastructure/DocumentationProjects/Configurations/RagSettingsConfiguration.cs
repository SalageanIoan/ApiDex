using ApiDex.Infrastructure.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiDex.Infrastructure.DocumentationProjects.Configurations;

public class RagSettingsConfiguration : IEntityTypeConfiguration<RagSettingsEntity>
{
    public void Configure(EntityTypeBuilder<RagSettingsEntity> builder)
    {
        builder.ToTable("RagSettings");

        builder.HasKey(settings => settings.Id);

        builder.Property(settings => settings.Id)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(settings => settings.ActiveModel)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(settings => settings.ContextMode)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(settings => settings.TopKChunks)
            .IsRequired();
    }
}
