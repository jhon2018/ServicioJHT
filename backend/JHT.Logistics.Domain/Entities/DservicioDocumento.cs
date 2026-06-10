using JHT.Logistics.Domain.Common;

namespace JHT.Logistics.Domain.Entities;

public class DservicioDocumento : AuditableEntity
{
    public int SerdocId { get; set; }
    public int SerId { get; set; }
    public int TipdocId { get; set; }
    
    public string? SerdocNumero { get; set; }
    public string SerdocArchivoUrl { get; set; } = null!; // URL de MinIO
    public string SerdocNombreOriginal { get; set; } = null!;
    public DateTime SerdocFechaCarga { get; set; }
    public string UsuarioCarga { get; set; } = null!;

    // Navigation Properties
    public Servicio Servicio { get; set; } = null!;
    public CTipoDocumento TipoDocumento { get; set; } = null!;
}
