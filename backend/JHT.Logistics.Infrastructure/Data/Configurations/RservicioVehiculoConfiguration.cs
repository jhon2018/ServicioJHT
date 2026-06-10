using JHT.Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JHT.Logistics.Infrastructure.Data.Configurations;

public class RservicioVehiculoConfiguration : IEntityTypeConfiguration<RservicioVehiculo>
{
    public void Configure(EntityTypeBuilder<RservicioVehiculo> builder)
    {
        builder.ToTable("TBL_RSERVICIO_VEHICULO");

        // Primary Key compuesta
        builder.HasKey(e => new { e.SerId, e.VehId });

        builder.Property(e => e.SerId).HasColumnName("SER_ID");
        builder.Property(e => e.VehId).HasColumnName("VEH_ID");
        builder.Property(e => e.FechaAsignacion).HasColumnName("FECHA_ASIGNACION").IsRequired();
        builder.Property(e => e.UsuarioAsignador).HasColumnName("USUARIO_ASIGNADOR").HasMaxLength(50).IsRequired();
        builder.Property(e => e.FechaInicio).HasColumnName("FECHA_INICIO");
        builder.Property(e => e.FechaFin).HasColumnName("FECHA_FIN");
        builder.Property(e => e.AsignacionActiva).HasColumnName("ASIGNACION_ACTIVA").IsRequired();
        builder.Property(e => e.MotivoCambio).HasColumnName("MOTIVO_CAMBIO").HasMaxLength(500);

        // Relationships
        builder.HasOne(r => r.Servicio)
            .WithMany(s => s.VehiculosAsignados)
            .HasForeignKey(r => r.SerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Vehiculo)
            .WithMany()
            .HasForeignKey(r => r.VehId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
