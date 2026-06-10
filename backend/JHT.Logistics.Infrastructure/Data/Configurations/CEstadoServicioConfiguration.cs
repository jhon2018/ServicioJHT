using JHT.Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JHT.Logistics.Infrastructure.Data.Configurations;

public class CEstadoServicioConfiguration : IEntityTypeConfiguration<CEstadoServicio>
{
    public void Configure(EntityTypeBuilder<CEstadoServicio> builder)
    {
        builder.ToTable("TBL_CESTADO_SERVICIO");

        builder.HasKey(e => e.EstId);

        builder.Property(e => e.EstId)
            .HasColumnName("EST_ID")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.EstCodigo)
            .HasColumnName("EST_CODIGO")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.EstNombre)
            .HasColumnName("EST_NOMBRE")
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(e => e.EstCodigo).IsUnique().HasDatabaseName("IX_CESTADO_CODIGO");

        // Seed Data
        builder.HasData(
            new CEstadoServicio { EstId = 1, EstCodigo = "RECEPCIONADO", EstNombre = "Recepcionado" },
            new CEstadoServicio { EstId = 2, EstCodigo = "PROGRAMADO", EstNombre = "Programado" },
            new CEstadoServicio { EstId = 3, EstCodigo = "UNIDAD_ASIGNADA", EstNombre = "Unidad Asignada" },
            new CEstadoServicio { EstId = 4, EstCodigo = "EN_RUTA", EstNombre = "En Ruta" },
            new CEstadoServicio { EstId = 5, EstCodigo = "MUY_CERCA", EstNombre = "Muy Cerca" },
            new CEstadoServicio { EstId = 6, EstCodigo = "ENTREGADO", EstNombre = "Entregado" },
            new CEstadoServicio { EstId = 7, EstCodigo = "CANCELADO", EstNombre = "Cancelado" }
        );
    }
}
