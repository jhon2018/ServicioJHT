using JHT.Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JHT.Logistics.Infrastructure.Data.Configurations;

public class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
{
    public void Configure(EntityTypeBuilder<Vehiculo> builder)
    {
        builder.ToTable("TBL_TVEHICULO");

        builder.HasKey(e => e.VehId);

        builder.Property(e => e.VehId)
            .HasColumnName("VEH_ID")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.VehPlaca)
            .HasColumnName("VEH_PLACA")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(e => e.VehMarca)
            .HasColumnName("VEH_MARCA")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.VehModelo)
            .HasColumnName("VEH_MODELO")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.VehTipo)
            .HasColumnName("VEH_TIPO")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.VehCapacidad)
            .HasColumnName("VEH_CAPACIDAD")
            .HasMaxLength(50);

        // Campos de AuditableEntity
        builder.Property(e => e.Estado)
            .HasColumnName("VEH_ESTADO")
            .IsRequired();

        builder.Property(e => e.FechaCreacion)
            .HasColumnName("VEH_FECHA_CREACION")
            .IsRequired();

        builder.Property(e => e.UsuarioCreacion)
            .HasColumnName("VEH_USUARIO_CREACION")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.FechaModificacion)
            .HasColumnName("VEH_FECHA_MODIFICACION");

        builder.Property(e => e.UsuarioModificacion)
            .HasColumnName("VEH_USUARIO_MODIFICACION")
            .HasMaxLength(50);

        // Índices Únicos obligatorios
        builder.HasIndex(e => e.VehPlaca)
            .IsUnique()
            .HasDatabaseName("IX_TVEHICULO_PLACA");

        // Global Query Filter for Soft Delete y estado operativo
        builder.HasQueryFilter(e => e.Estado);
    }
}
