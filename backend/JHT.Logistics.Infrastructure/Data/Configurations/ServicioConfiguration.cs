using JHT.Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JHT.Logistics.Infrastructure.Data.Configurations;

public class ServicioConfiguration : IEntityTypeConfiguration<Servicio>
{
    public void Configure(EntityTypeBuilder<Servicio> builder)
    {
        builder.ToTable("TBL_TSERVICIO");

        builder.HasKey(e => e.SerId);

        builder.Property(e => e.SerId).HasColumnName("SER_ID").ValueGeneratedOnAdd();
        builder.Property(e => e.SerCodigo).HasColumnName("SER_CODIGO").HasMaxLength(50).IsRequired();
        builder.Property(e => e.CliId).HasColumnName("CLI_ID").IsRequired();
        builder.Property(e => e.SerTipoServicio).HasColumnName("SER_TIPO_SERVICIO").HasMaxLength(50).IsRequired();
        builder.Property(e => e.SerDescripcion).HasColumnName("SER_DESCRIPCION").HasMaxLength(500);
        builder.Property(e => e.SerObservacion).HasColumnName("SER_OBSERVACION").HasMaxLength(1000);
        builder.Property(e => e.SerPrioridad).HasColumnName("SER_PRIORIDAD").HasMaxLength(20);
        
        builder.Property(e => e.SerFechaProgramada).HasColumnName("SER_FECHA_PROGRAMADA");
        builder.Property(e => e.SerFechaInicioReal).HasColumnName("SER_FECHA_INICIO_REAL");
        builder.Property(e => e.SerFechaFinReal).HasColumnName("SER_FECHA_FIN_REAL");
        builder.Property(e => e.EstId).HasColumnName("EST_ID").IsRequired();

        // Auditable fields
        builder.Property(e => e.Estado).HasColumnName("SER_ESTADO_LOGICO").IsRequired();
        builder.Property(e => e.FechaCreacion).HasColumnName("SER_FECHA_CREACION").IsRequired();
        builder.Property(e => e.UsuarioCreacion).HasColumnName("SER_USUARIO_CREACION").HasMaxLength(50).IsRequired();
        builder.Property(e => e.FechaModificacion).HasColumnName("SER_FECHA_MODIFICACION");
        builder.Property(e => e.UsuarioModificacion).HasColumnName("SER_USUARIO_MODIFICACION").HasMaxLength(50);

        builder.HasIndex(e => e.SerCodigo).IsUnique().HasDatabaseName("IX_TSERVICIO_CODIGO");

        // Relationships
        builder.HasOne(s => s.Cliente)
            .WithMany()
            .HasForeignKey(s => s.CliId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.EstadoServicio)
            .WithMany()
            .HasForeignKey(s => s.EstId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(e => e.Estado);
    }
}
