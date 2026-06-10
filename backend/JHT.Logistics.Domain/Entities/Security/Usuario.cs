using JHT.Logistics.Domain.Common;

namespace JHT.Logistics.Domain.Entities.Security;

public class Usuario : AuditableEntity
{
    public int UsuId { get; set; }
    public string UsuLogin { get; set; } = null!;
    public string UsuNombre { get; set; } = null!;
    public string UsuEmail { get; set; } = null!;
    public string UsuPasswordHash { get; set; } = null!;
    
    // Relación
    public virtual ICollection<UsuarioRol> UsuariosRoles { get; set; } = new List<UsuarioRol>();
}
