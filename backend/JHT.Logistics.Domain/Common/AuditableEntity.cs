namespace JHT.Logistics.Domain.Common;

public abstract class AuditableEntity
{
    public DateTime FechaCreacion { get; set; }
    public string UsuarioCreacion { get; set; } = null!;
    public DateTime? FechaModificacion { get; set; }
    public string? UsuarioModificacion { get; set; }
    public bool Estado { get; set; } = true;
}
