using JHT.Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JHT.Logistics.Infrastructure.Data.Configurations;

public class AuditoriaLogConfiguration : IEntityTypeConfiguration<AuditoriaLog>
{
    public void Configure(EntityTypeBuilder<AuditoriaLog> builder)
    {
        builder.ToTable("TBL_AAUDITORIA");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("AUD_ID").ValueGeneratedOnAdd();
        builder.Property(e => e.NombreTabla).HasColumnName("AUD_TABLA").HasMaxLength(100).IsRequired();
        builder.Property(e => e.RegistroId).HasColumnName("AUD_REGISTRO_ID").HasMaxLength(100).IsRequired();
        builder.Property(e => e.Accion).HasColumnName("AUD_ACCION").HasMaxLength(20).IsRequired();
        builder.Property(e => e.Usuario).HasColumnName("AUD_USUARIO").HasMaxLength(100).IsRequired();
        builder.Property(e => e.Fecha).HasColumnName("AUD_FECHA").IsRequired();
        
        // Uso de tipo jsonb en PostgreSQL
        builder.Property(e => e.ValoresAnteriores).HasColumnName("AUD_VALORES_ANTERIORES").HasColumnType("jsonb");
        builder.Property(e => e.ValoresNuevos).HasColumnName("AUD_VALORES_NUEVOS").HasColumnType("jsonb");

        // Índices para optimizar búsquedas por tabla o registro
        builder.HasIndex(e => e.NombreTabla).HasDatabaseName("IX_AAUDITORIA_TABLA");
        builder.HasIndex(e => e.RegistroId).HasDatabaseName("IX_AAUDITORIA_REGISTROID");
    }
}
