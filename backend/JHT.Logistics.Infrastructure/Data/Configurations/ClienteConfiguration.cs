using JHT.Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JHT.Logistics.Infrastructure.Data.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("TBL_TCLIENTE");

        builder.HasKey(e => e.CliId);

        builder.Property(e => e.CliId)
            .HasColumnName("CLI_ID")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.CliTipoDocumento)
            .HasColumnName("CLI_TIPO_DOCUMENTO")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(e => e.CliNumeroDocumento)
            .HasColumnName("CLI_NUMERO_DOCUMENTO")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.CliRazonSocial)
            .HasColumnName("CLI_RAZON_SOCIAL")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.CliNombreComercial)
            .HasColumnName("CLI_NOMBRE_COMERCIAL")
            .HasMaxLength(255);

        builder.Property(e => e.CliDireccion)
            .HasColumnName("CLI_DIRECCION")
            .HasMaxLength(500);

        builder.Property(e => e.CliTelefono)
            .HasColumnName("CLI_TELEFONO")
            .HasMaxLength(50);

        builder.Property(e => e.CliEmail)
            .HasColumnName("CLI_EMAIL")
            .HasMaxLength(150);

        // Campos de AuditableEntity
        builder.Property(e => e.Estado)
            .HasColumnName("CLI_ESTADO")
            .IsRequired();

        builder.Property(e => e.FechaCreacion)
            .HasColumnName("CLI_FECHA_CREACION")
            .IsRequired();

        builder.Property(e => e.UsuarioCreacion)
            .HasColumnName("CLI_USUARIO_CREACION")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.FechaModificacion)
            .HasColumnName("CLI_FECHA_MODIFICACION");

        builder.Property(e => e.UsuarioModificacion)
            .HasColumnName("CLI_USUARIO_MODIFICACION")
            .HasMaxLength(50);

        // Índices
        builder.HasIndex(e => e.CliNumeroDocumento)
            .IsUnique()
            .HasDatabaseName("IX_TCLIENTE_NUMERO_DOCUMENTO");

        // Global Query Filter for Soft Delete
        builder.HasQueryFilter(e => e.Estado);
    }
}
