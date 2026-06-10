using JHT.Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JHT.Logistics.Infrastructure.Data.Configurations;

public class CTipoDocumentoConfiguration : IEntityTypeConfiguration<CTipoDocumento>
{
    public void Configure(EntityTypeBuilder<CTipoDocumento> builder)
    {
        builder.ToTable("TBL_CTIPO_DOCUMENTO");

        builder.HasKey(e => e.TipdocId);

        builder.Property(e => e.TipdocId)
            .HasColumnName("TIPDOC_ID")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.TipdocCodigo)
            .HasColumnName("TIPDOC_CODIGO")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.TipdocNombre)
            .HasColumnName("TIPDOC_NOMBRE")
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(e => e.TipdocCodigo).IsUnique().HasDatabaseName("IX_CTIPDOC_CODIGO");

        // Seed Data
        builder.HasData(
            new CTipoDocumento { TipdocId = 1, TipdocCodigo = "GRE", TipdocNombre = "Guía de Remisión" },
            new CTipoDocumento { TipdocId = 2, TipdocCodigo = "FACTURA", TipdocNombre = "Factura" },
            new CTipoDocumento { TipdocId = 3, TipdocCodigo = "BOLETA", TipdocNombre = "Boleta" },
            new CTipoDocumento { TipdocId = 4, TipdocCodigo = "ORDEN_SERVICIO", TipdocNombre = "Orden de Servicio" },
            new CTipoDocumento { TipdocId = 5, TipdocCodigo = "ORDEN_COMPRA", TipdocNombre = "Orden de Compra" },
            new CTipoDocumento { TipdocId = 6, TipdocCodigo = "CARGO", TipdocNombre = "Cargo de Entrega" },
            new CTipoDocumento { TipdocId = 7, TipdocCodigo = "EVIDENCIA", TipdocNombre = "Evidencia Fotográfica" },
            new CTipoDocumento { TipdocId = 8, TipdocCodigo = "OTRO", TipdocNombre = "Otro Documento" }
        );
    }
}
