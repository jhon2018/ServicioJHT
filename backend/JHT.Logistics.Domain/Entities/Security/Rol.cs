using JHT.Logistics.Domain.Common;

namespace JHT.Logistics.Domain.Entities.Security;

public class Rol : AuditableEntity
{
    public int RolId { get; set; }
    public string RolNombre { get; set; } = null!;
    public string RolDescripcion { get; set; } = null!;
    
    // Relación
    public virtual ICollection<UsuarioRol> UsuariosRoles { get; set; } = new List<UsuarioRol>();
}
