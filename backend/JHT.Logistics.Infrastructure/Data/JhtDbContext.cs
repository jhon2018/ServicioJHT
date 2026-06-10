using System.Reflection;
using JHT.Logistics.Domain.Entities.Security;
using JHT.Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JHT.Logistics.Infrastructure.Data;

public class JhtDbContext : DbContext
{
    public JhtDbContext(DbContextOptions<JhtDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Rol> Roles => Set<Rol>();
    public DbSet<UsuarioRol> UsuariosRoles => Set<UsuarioRol>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Conductor> Conductores => Set<Conductor>();
    public DbSet<Vehiculo> Vehiculos => Set<Vehiculo>();
    
    // Módulo de Servicios
    public DbSet<CEstadoServicio> EstadosServicio => Set<CEstadoServicio>();
    public DbSet<CTipoDocumento> TiposDocumento => Set<CTipoDocumento>();
    public DbSet<Servicio> Servicios => Set<Servicio>();
    public DbSet<DservicioDestino> Destinos => Set<DservicioDestino>();
    public DbSet<DservicioDocumento> Documentos => Set<DservicioDocumento>();
    public DbSet<HservicioEstado> HistorialEstados => Set<HservicioEstado>();
    public DbSet<TTrackingPublico> TrackingPublico => Set<TTrackingPublico>();
    public DbSet<RservicioConductor> AsignacionesConductores => Set<RservicioConductor>();
    public DbSet<RservicioVehiculo> AsignacionesVehiculos => Set<RservicioVehiculo>();
    
    // Módulo de Auditoría
    public DbSet<AuditoriaLog> AuditoriaLogs => Set<AuditoriaLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Aplica todas las configuraciones IEntityTypeConfiguration<T> que estén en este assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
