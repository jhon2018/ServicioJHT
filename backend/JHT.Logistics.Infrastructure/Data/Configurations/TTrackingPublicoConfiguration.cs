using JHT.Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JHT.Logistics.Infrastructure.Data.Configurations;

public class TTrackingPublicoConfiguration : IEntityTypeConfiguration<TTrackingPublico>
{
    public void Configure(EntityTypeBuilder<TTrackingPublico> builder)
    {
        builder.ToTable("TBL_TTRACKING_PUBLICO");

        builder.HasKey(e => e.TrkId);

        builder.Property(e => e.TrkId).HasColumnName("TRK_ID").ValueGeneratedOnAdd();
        builder.Property(e => e.SerId).HasColumnName("SER_ID").IsRequired();
        builder.Property(e => e.TrkToken).HasColumnName("TRK_TOKEN").HasMaxLength(50).IsRequired();
        builder.Property(e => e.TrkFechaCreacion).HasColumnName("TRK_FECHA_CREACION").IsRequired();
        builder.Property(e => e.TrkEstado).HasColumnName("TRK_ESTADO").IsRequired();

        builder.HasIndex(e => e.TrkToken).IsUnique().HasDatabaseName("IX_TTRACKING_TOKEN");
        builder.HasIndex(e => e.SerId).IsUnique().HasDatabaseName("IX_TTRACKING_SERVICIO"); // Relación 1:1 conceptual

        // Relationships
        builder.HasOne(t => t.Servicio)
            .WithOne(s => s.TrackingPublico)
            .HasForeignKey<TTrackingPublico>(t => t.SerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
