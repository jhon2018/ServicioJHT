namespace JHT.Logistics.Domain.Entities;

public class AuditoriaLog
{
    public int Id { get; set; }
    public string NombreTabla { get; set; } = null!;
    public string RegistroId { get; set; } = null!;
    public string Accion { get; set; } = null!; // INSERT, UPDATE, DELETE
    public string Usuario { get; set; } = null!;
    public DateTime Fecha { get; set; }
    public string? ValoresAnteriores { get; set; } // JSON
    public string? ValoresNuevos { get; set; } // JSON
}
