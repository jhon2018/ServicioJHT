using JHT.Logistics.Application.Features.Servicios.DTOs;
using JHT.Logistics.Application.Interfaces.Repositories;
using JHT.Logistics.Domain.Entities;
using MediatR;
using System.Security.Cryptography;

namespace JHT.Logistics.Application.Features.Servicios.Commands;

public class CreateServicioCommand : IRequest<ServicioDto>
{
    public int CliId { get; set; }
    public string SerTipoServicio { get; set; } = null!;
    public string? SerDescripcion { get; set; }
    public string? SerObservacion { get; set; }
    public string? SerPrioridad { get; set; }
    public DateTime? SerFechaProgramada { get; set; }
    
    public List<CreateDestinoDto> Destinos { get; set; } = new List<CreateDestinoDto>();
}

public class CreateDestinoDto
{
    public int Secuencia { get; set; }
    public string Destinatario { get; set; } = null!;
    public string? Ruc { get; set; }
    public string Direccion { get; set; } = null!;
    public string? Referencia { get; set; }
    public string? Contacto { get; set; }
    public string? Telefono { get; set; }
}

public class CreateServicioCommandHandler : IRequestHandler<CreateServicioCommand, ServicioDto>
{
    private readonly IServicioRepository _repository;

    public CreateServicioCommandHandler(IServicioRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServicioDto> Handle(CreateServicioCommand request, CancellationToken cancellationToken)
    {
        // 1. Generar Código Único
        string codigo = $"JHT-{DateTime.UtcNow.Year}-{Guid.NewGuid().ToString().Substring(0, 6).ToUpper()}";

        // 2. Crear Entidad Servicio (Estado 1 = RECEPCIONADO)
        var servicio = new Servicio
        {
            SerCodigo = codigo,
            CliId = request.CliId,
            SerTipoServicio = request.SerTipoServicio,
            SerDescripcion = request.SerDescripcion,
            SerObservacion = request.SerObservacion,
            SerPrioridad = request.SerPrioridad,
            SerFechaProgramada = request.SerFechaProgramada,
            EstId = 1, 
            Estado = true
        };

        // 3. Crear Destinos
        foreach (var dest in request.Destinos.OrderBy(d => d.Secuencia))
        {
            servicio.Destinos.Add(new DservicioDestino
            {
                SerdesSecuencia = dest.Secuencia,
                SerdesDestinatario = dest.Destinatario,
                SerdesRuc = dest.Ruc,
                SerdesDireccion = dest.Direccion,
                SerdesReferencia = dest.Referencia,
                SerdesContacto = dest.Contacto,
                SerdesTelefono = dest.Telefono,
                SerdesEstado = "PENDIENTE",
                Estado = true
            });
        }

        // 4. Agregar Historial Inicial
        servicio.HistorialEstados.Add(new HservicioEstado
        {
            EstId = 1, // RECEPCIONADO
            HseFechaHora = DateTime.UtcNow,
            HseObservacion = "Creación de Orden de Servicio",
            UsuarioRegistro = "SISTEMA" // Idealmente sacado del Claim JWT
        });

        // 5. Generar Token Público de Tracking (Ej: JHT-Xy8F9)
        servicio.TrackingPublico = new TTrackingPublico
        {
            TrkToken = $"JHT-{Convert.ToHexString(RandomNumberGenerator.GetBytes(4))}",
            TrkFechaCreacion = DateTime.UtcNow,
            TrkEstado = true
        };

        // 6. Guardar en Base de Datos
        await _repository.AddAsync(servicio, cancellationToken);

        // Volver a consultar para incluir catálogos si se requiere, pero por eficiencia mapeamos directamente
        var servicioDb = await _repository.GetByIdAsync(servicio.SerId, cancellationToken);
        
        return servicioDb!.ToDto();
    }
}
