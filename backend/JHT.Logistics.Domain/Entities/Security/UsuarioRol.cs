namespace JHT.Logistics.Domain.Entities.Security;

public class UsuarioRol
{
    public int UsrId { get; set; } // Identificador principal de la relación (id autonumérico opcional o composite)
    // Aquí el documento dice:
    // USR_ID
    // ROL_ID
    // Observamos que en USUARIO era USU_ID. Para mantener consistencia con DOC-007 usaremos UsuId o UsrId según corresponda, pero usaremos propiedades en PascalCase y las configuraremos en FluentAPI
    
    public int UsuId { get; set; }
    public virtual Usuario Usuario { get; set; } = null!;

    public int RolId { get; set; }
    public virtual Rol Rol { get; set; } = null!;
}
