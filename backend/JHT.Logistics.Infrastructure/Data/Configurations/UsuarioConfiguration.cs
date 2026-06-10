using JHT.Logistics.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JHT.Logistics.Infrastructure.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("TBL_UUSUARIO");

        builder.HasKey(e => e.UsuId);
        
        builder.Property(e => e.UsuId)
            .HasColumnName("USU_ID")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.UsuLogin)
            .HasColumnName("USU_LOGIN")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.UsuNombre)
            .HasColumnName("USU_NOMBRE")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.UsuEmail)
            .HasColumnName("USU_EMAIL")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(e => e.UsuPasswordHash)
            .HasColumnName("USU_PASSWORD_HASH")
            .IsRequired();

        builder.Property(e => e.Estado)
            .HasColumnName("USU_ESTADO")
            .IsRequired();

        builder.Property(e => e.FechaCreacion)
            .HasColumnName("USU_FECHA_CREACION")
            .IsRequired();

        builder.Property(e => e.UsuarioCreacion)
            .HasColumnName("USU_USUARIO_CREACION")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.FechaModificacion)
            .HasColumnName("USU_FECHA_MODIFICACION");

        builder.Property(e => e.UsuarioModificacion)
            .HasColumnName("USU_USUARIO_MODIFICACION")
            .HasMaxLength(50);
            
        // Índices Únicos
        builder.HasIndex(e => e.UsuLogin)
            .IsUnique()
            .HasDatabaseName("IX_UUSUARIO_LOGIN");
            
        builder.HasIndex(e => e.UsuEmail)
            .IsUnique()
            .HasDatabaseName("IX_UUSUARIO_EMAIL");

        // Seed Initial Admin User
        var now = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc); // Use static date for EF Core Migrations
        builder.HasData(
            new Usuario 
            { 
                UsuId = 1, 
                UsuLogin = "admin", 
                UsuNombre = "Administrador del Sistema", 
                UsuEmail = "admin@jhtlogistics.com", 
                UsuPasswordHash = "$2a$11$V.ZsAZaVSCnCTeiRY9u26OhFGxsL3/.ffZmNWC4VI5yjDo.X30dHa", 
                FechaCreacion = now, 
                UsuarioCreacion = "SYSTEM", 
                Estado = true 
            }
        );

        // Global Query Filter for Soft Delete
        builder.HasQueryFilter(e => e.Estado);
    }
}
