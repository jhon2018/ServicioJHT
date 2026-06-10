using JHT.Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JHT.Logistics.Infrastructure.Data.Configurations;

public class DservicioDocumentoConfiguration : IEntityTypeConfiguration<DservicioDocumento>
{
    public void Configure(EntityTypeBuilder<DservicioDocumento> builder)
    {
        builder.ToTable("TBL_DSERVICIO_DOCUMENTO");

        builder.HasKey(e => e.SerdocId);

        builder.Property(e => e.SerdocId).HasColumnName("SERDOC_ID").ValueGeneratedOnAdd();
        builder.Property(e => e.SerId).HasColumnName("SER_ID").IsRequired();
        builder.Property(e => e.TipdocId).HasColumnName("TIPDOC_ID").IsRequired();
        builder.Property(e => e.SerdocNumero).HasColumnName("SERDOC_NUMERO").HasMaxLength(100);
        builder.Property(e => e.SerdocArchivoUrl).HasColumnName("SERDOC_ARCHIVO_URL").HasMaxLength(1000).IsRequired();
        builder.Property(e => e.SerdocNombreOriginal).HasColumnName("SERDOC_NOMBRE_ORIGINAL").HasMaxLength(255).IsRequired();
        builder.Property(e => e.SerdocFechaCarga).HasColumnName("SERDOC_FECHA_CARGA").IsRequired();
        builder.Property(e => e.UsuarioCarga).HasColumnName("USUARIO_CARGA").HasMaxLength(50).IsRequired();

        // Auditable fields
        builder.Property(e => e.Estado).HasColumnName("SERDOC_ESTADO_LOGICO").IsRequired();
        builder.Property(e => e.FechaCreacion).HasColumnName("SERDOC_FECHA_CREACION").IsRequired();
        builder.Property(e => e.UsuarioCreacion).HasColumnName("SERDOC_USUARIO_CREACION").HasMaxLength(50).IsRequired();
        builder.Property(e => e.FechaModificacion).HasColumnName("SERDOC_FECHA_MODIFICACION");
        builder.Property(e => e.UsuarioModificacion).HasColumnName("SERDOC_USUARIO_MODIFICACION").HasMaxLength(50);

        // Relationships
        builder.HasOne(d => d.Servicio)
            .WithMany(s => s.Documentos)
            .HasForeignKey(d => d.SerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(d => d.TipoDocumento)
            .WithMany()
            .HasForeignKey(d => d.TipdocId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(e => e.Estado);
    }
}
