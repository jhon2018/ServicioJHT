using JHT.Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JHT.Logistics.Infrastructure.Data.Configurations;

public class HservicioEstadoConfiguration : IEntityTypeConfiguration<HservicioEstado>
{
    public void Configure(EntityTypeBuilder<HservicioEstado> builder)
    {
        builder.ToTable("TBL_HSERVICIO_ESTADO");

        builder.HasKey(e => e.HseId);

        builder.Property(e => e.HseId).HasColumnName("HSE_ID").ValueGeneratedOnAdd();
        builder.Property(e => e.SerId).HasColumnName("SER_ID").IsRequired();
        builder.Property(e => e.EstId).HasColumnName("EST_ID").IsRequired();
        builder.Property(e => e.HseFechaHora).HasColumnName("HSE_FECHA_HORA").IsRequired();
        builder.Property(e => e.HseObservacion).HasColumnName("HSE_OBSERVACION").HasMaxLength(500);
        builder.Property(e => e.UsuarioRegistro).HasColumnName("USUARIO_REGISTRO").HasMaxLength(50).IsRequired();

        // Relationships
        builder.HasOne(h => h.Servicio)
            .WithMany(s => s.HistorialEstados)
            .HasForeignKey(h => h.SerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(h => h.EstadoServicio)
            .WithMany()
            .HasForeignKey(h => h.EstId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
