using JHT.Logistics.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JHT.Logistics.Infrastructure.Data.Configurations;

public class UsuarioRolConfiguration : IEntityTypeConfiguration<UsuarioRol>
{
    public void Configure(EntityTypeBuilder<UsuarioRol> builder)
    {
        builder.ToTable("TBL_RUSUARIO_ROL");

        // PK Compuesta
        builder.HasKey(e => new { e.UsrId, e.RolId });

        builder.Property(e => e.UsrId)
            .HasColumnName("USR_ID"); // Esto viene del DOC-007, pero es FK a TBL_UUSUARIO.USU_ID

        builder.Property(e => e.RolId)
            .HasColumnName("ROL_ID");

        builder.Property(e => e.UsuId)
            .HasColumnName("USU_ID"); // Added to handle proper FK mapping if USR_ID is not the FK

        // En DOC-007 dice:
        // TBL_RUSUARIO_ROL
        // USR_ID
        // ROL_ID
        // Vamos a asumir que USR_ID es la clave primaria o el ID de la tabla de la relación y que la FK hacia USUARIO es USU_ID. 
        // Para simplificar, haremos que UsrId sea la FK hacia Usuario.UsuId
        
        builder.HasOne(e => e.Usuario)
            .WithMany(u => u.UsuariosRoles)
            .HasForeignKey(e => e.UsrId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Rol)
            .WithMany(r => r.UsuariosRoles)
            .HasForeignKey(e => e.RolId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // Ignore the extra property to strictly map UsrId as the foreign key
        builder.Ignore(e => e.UsuId);

        // Seed Root Admin Role
        builder.HasData(
            new UsuarioRol 
            { 
                UsrId = 1, 
                RolId = 1 
            }
        );
    }
}
