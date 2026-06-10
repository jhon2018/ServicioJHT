using JHT.Logistics.Application.Features.Dashboard.DTOs;
using JHT.Logistics.Application.Interfaces.Repositories;
using MediatR;

namespace JHT.Logistics.Application.Features.Dashboard.Queries;

public class GetDashboardMetricsQuery : IRequest<DashboardMetricsResponse>
{
}

public class GetDashboardMetricsQueryHandler : IRequestHandler<GetDashboardMetricsQuery, DashboardMetricsResponse>
{
    private readonly IServicioRepository _servicioRepository;

    public GetDashboardMetricsQueryHandler(IServicioRepository servicioRepository)
    {
        _servicioRepository = servicioRepository;
    }

    public async Task<DashboardMetricsResponse> Handle(GetDashboardMetricsQuery request, CancellationToken cancellationToken)
    {
        var allServicios = await _servicioRepository.GetAllWithStatesAsync(cancellationToken);
        var now = DateTime.UtcNow;

        int serviciosActivos = 0;
        int unidadesEnRuta = 0;
        int entregasHoy = 0;

        foreach (var svc in allServicios)
        {
            var codigoEstado = svc.EstadoServicio?.EstCodigo ?? "";

            // Servicios Activos: Ni ENTREGADO ni CANCELADO
            if (codigoEstado != "ENTREGADO" && codigoEstado != "CANCELADO")
            {
                serviciosActivos++;
            }

            // Unidades en Ruta
            if (codigoEstado == "EN_RUTA")
            {
                unidadesEnRuta++;
            }

            // Entregas Hoy (ENTREGADO y SerFechaFinReal es de hoy)
            if (codigoEstado == "ENTREGADO" && svc.SerFechaFinReal.HasValue)
            {
                if (svc.SerFechaFinReal.Value.Date == now.Date)
                {
                    entregasHoy++;
                }
            }
        }

        return new DashboardMetricsResponse
        {
            ServiciosActivos = serviciosActivos,
            UnidadesEnRuta = unidadesEnRuta,
            EntregasHoy = entregasHoy
        };
    }
}
