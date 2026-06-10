using JHT.Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JHT.Logistics.Infrastructure.Data.Configurations;

public class DservicioDestinoConfiguration : IEntityTypeConfiguration<DservicioDestino>
{
    public void Configure(EntityTypeBuilder<DservicioDestino> builder)
    {
        builder.ToTable("TBL_DSERVICIO_DESTINO");

        builder.HasKey(e => e.SerdesId);

        builder.Property(e => e.SerdesId).HasColumnName("SERDES_ID").ValueGeneratedOnAdd();
        builder.Property(e => e.SerId).HasColumnName("SER_ID").IsRequired();
        builder.Property(e => e.SerdesSecuencia).HasColumnName("SERDES_SECUENCIA").IsRequired();
        builder.Property(e => e.SerdesDestinatario).HasColumnName("SERDES_DESTINATARIO").HasMaxLength(255).IsRequired();
        builder.Property(e => e.SerdesRuc).HasColumnName("SERDES_RUC").HasMaxLength(20);
        builder.Property(e => e.SerdesDireccion).HasColumnName("SERDES_DIRECCION").HasMaxLength(500).IsRequired();
        builder.Property(e => e.SerdesReferencia).HasColumnName("SERDES_REFERENCIA").HasMaxLength(500);
        builder.Property(e => e.SerdesContacto).HasColumnName("SERDES_CONTACTO").HasMaxLength(150);
        builder.Property(e => e.SerdesTelefono).HasColumnName("SERDES_TELEFONO").HasMaxLength(50);
        builder.Property(e => e.SerdesEstado).HasColumnName("SERDES_ESTADO").HasMaxLength(50).IsRequired();

        // Auditable fields
        builder.Property(e => e.Estado).HasColumnName("SERDES_ESTADO_LOGICO").IsRequired();
        builder.Property(e => e.FechaCreacion).HasColumnName("SERDES_FECHA_CREACION").IsRequired();
        builder.Property(e => e.UsuarioCreacion).HasColumnName("SERDES_USUARIO_CREACION").HasMaxLength(50).IsRequired();
        builder.Property(e => e.FechaModificacion).HasColumnName("SERDES_FECHA_MODIFICACION");
        builder.Property(e => e.UsuarioModificacion).HasColumnName("SERDES_USUARIO_MODIFICACION").HasMaxLength(50);

        // Relationships
        builder.HasOne(d => d.Servicio)
            .WithMany(s => s.Destinos)
            .HasForeignKey(d => d.SerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(e => e.Estado);
    }
}
