using System.Security.Claims;
using System.Text.Json;
using JHT.Logistics.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace JHT.Logistics.Infrastructure.Data.Interceptors;

public class AuditoriaSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    // Tablas permitidas para auditar
    private readonly HashSet<string> _tablasAuditables = new HashSet<string>
    {
        "TBL_TCLIENTE",
        "TBL_TCONDUCTOR",
        "TBL_TVEHICULO",
        "TBL_TSERVICIO",
        "TBL_DSERVICIO_DESTINO",
        "TBL_DSERVICIO_DOCUMENTO",
        "TBL_RSERVICIO_CONDUCTOR",
        "TBL_RSERVICIO_VEHICULO"
    };

    public AuditoriaSaveChangesInterceptor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ProcesarAuditoria(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await ProcesarAuditoria(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task ProcesarAuditoria(DbContext? context)
    {
        if (context == null) return;

        context.ChangeTracker.DetectChanges();

        var entradasAuditoria = new List<AuditoriaLog>();

        var usuarioActual = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value 
                         ?? _httpContextAccessor.HttpContext?.User?.FindFirst("Nombre")?.Value 
                         ?? "SYSTEM";

        var entries = context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
            .ToList();

        foreach (var entry in entries)
        {
            var nombreTabla = entry.Metadata.GetTableName() ?? "Desconocida";

            // Si la tabla no está en nuestra lista blanca, la ignoramos.
            if (!_tablasAuditables.Contains(nombreTabla))
                continue;

            // Extraer la Primary Key
            var pk = entry.Metadata.FindPrimaryKey();
            var registroId = pk != null ? string.Join("-", pk.Properties.Select(p => entry.Property(p.Name).CurrentValue?.ToString())) : "N/A";

            string accion = entry.State switch
            {
                EntityState.Added => "INSERT",
                EntityState.Modified => "UPDATE",
                EntityState.Deleted => "DELETE",
                _ => "UNKNOWN"
            };

            var auditLog = new AuditoriaLog
            {
                NombreTabla = nombreTabla,
                Accion = accion,
                Usuario = usuarioActual,
                Fecha = DateTime.UtcNow,
                RegistroId = registroId
            };

            var valoresAnteriores = new Dictionary<string, object?>();
            var valoresNuevos = new Dictionary<string, object?>();

            foreach (var property in entry.Properties)
            {
                // Ignorar propiedades de navegación o temporales
                if (property.IsTemporary) continue;

                string propName = property.Metadata.Name;

                switch (entry.State)
                {
                    case EntityState.Added:
                        valoresNuevos[propName] = property.CurrentValue;
                        break;
                    case EntityState.Deleted:
                        valoresAnteriores[propName] = property.OriginalValue;
                        break;
                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            valoresAnteriores[propName] = property.OriginalValue;
                            valoresNuevos[propName] = property.CurrentValue;
                        }
                        break;
                }
            }

            auditLog.ValoresAnteriores = valoresAnteriores.Count > 0 ? JsonSerializer.Serialize(valoresAnteriores) : null;
            auditLog.ValoresNuevos = valoresNuevos.Count > 0 ? JsonSerializer.Serialize(valoresNuevos) : null;

            entradasAuditoria.Add(auditLog);
        }

        if (entradasAuditoria.Any())
        {
            await context.AddRangeAsync(entradasAuditoria);
        }
    }
}
