using JHT.Logistics.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JHT.Logistics.Infrastructure.Data.Configurations;

public class RolConfiguration : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        builder.ToTable("TBL_CROL");

        builder.HasKey(e => e.RolId);

        builder.Property(e => e.RolId)
            .HasColumnName("ROL_ID")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.RolNombre)
            .HasColumnName("ROL_NOMBRE")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.RolDescripcion)
            .HasColumnName("ROL_DESCRIPCION")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.Estado)
            .HasColumnName("ROL_ESTADO")
            .IsRequired();

        builder.Property(e => e.FechaCreacion)
            .HasColumnName("ROL_FECHA_CREACION")
            .IsRequired();

        builder.Property(e => e.UsuarioCreacion)
            .HasColumnName("ROL_USUARIO_CREACION")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.FechaModificacion)
            .HasColumnName("ROL_FECHA_MODIFICACION");

        builder.Property(e => e.UsuarioModificacion)
            .HasColumnName("ROL_USUARIO_MODIFICACION")
            .HasMaxLength(50);
            
        // Índices Únicos
        builder.HasIndex(e => e.RolNombre)
            .IsUnique()
            .HasDatabaseName("IX_CROL_NOMBRE");
            
        // Seed Initial Roles
        builder.HasData(
            new Rol { RolId = 1, RolNombre = "ADMINISTRADOR", RolDescripcion = "Control total del sistema", FechaCreacion = DateTime.UtcNow, UsuarioCreacion = "SYSTEM", Estado = true },
            new Rol { RolId = 2, RolNombre = "OPERADOR", RolDescripcion = "Operación diaria", FechaCreacion = DateTime.UtcNow, UsuarioCreacion = "SYSTEM", Estado = true },
            new Rol { RolId = 3, RolNombre = "CONDUCTOR", RolDescripcion = "Uso desde aplicación Flutter", FechaCreacion = DateTime.UtcNow, UsuarioCreacion = "SYSTEM", Estado = true }
        );

        builder.HasQueryFilter(e => e.Estado);
    }
}
