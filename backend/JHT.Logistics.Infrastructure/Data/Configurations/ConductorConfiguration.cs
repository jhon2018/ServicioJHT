using JHT.Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JHT.Logistics.Infrastructure.Data.Configurations;

public class ConductorConfiguration : IEntityTypeConfiguration<Conductor>
{
    public void Configure(EntityTypeBuilder<Conductor> builder)
    {
        builder.ToTable("TBL_TCONDUCTOR");

        builder.HasKey(e => e.ConId);

        builder.Property(e => e.ConId)
            .HasColumnName("CON_ID")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.ConCodigoExterno)
            .HasColumnName("CON_CODIGO_EXTERNO")
            .HasMaxLength(50);

        builder.Property(e => e.ConTipo)
            .HasColumnName("CON_TIPO")
            .HasMaxLength(1)
            .IsRequired();

        builder.Property(e => e.ConDni)
            .HasColumnName("CON_DNI")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(e => e.ConNombreCompleto)
            .HasColumnName("CON_NOMBRE_COMPLETO")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.ConTelefono)
            .HasColumnName("CON_TELEFONO")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.ConEmail)
            .HasColumnName("CON_EMAIL")
            .HasMaxLength(150);

        // Campos de AuditableEntity
        builder.Property(e => e.Estado)
            .HasColumnName("CON_ESTADO")
            .IsRequired();

        builder.Property(e => e.FechaCreacion)
            .HasColumnName("CON_FECHA_CREACION")
            .IsRequired();

        builder.Property(e => e.UsuarioCreacion)
            .HasColumnName("CON_USUARIO_CREACION")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.FechaModificacion)
            .HasColumnName("CON_FECHA_MODIFICACION");

        builder.Property(e => e.UsuarioModificacion)
            .HasColumnName("CON_USUARIO_MODIFICACION")
            .HasMaxLength(50);

        // Índices Únicos
        builder.HasIndex(e => e.ConDni)
            .IsUnique()
            .HasDatabaseName("IX_TCONDUCTOR_DNI");

        // Global Query Filter for Soft Delete
        builder.HasQueryFilter(e => e.Estado);
    }
}
