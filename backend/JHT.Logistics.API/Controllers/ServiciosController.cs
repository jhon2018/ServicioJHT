using JHT.Logistics.Application.Features.Servicios.Commands;
using JHT.Logistics.Application.Features.Servicios.DTOs;
using JHT.Logistics.Application.Features.Servicios.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JHT.Logistics.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "ADMINISTRADOR,OPERADOR,CONDUCTOR")]
public class ServiciosController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServiciosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ServicioDto>))]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllServiciosQuery());
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ServicioDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "ADMINISTRADOR,OPERADOR")]
    public async Task<IActionResult> Create([FromBody] CreateServicioCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id = result.SerId }, result); // Simplificado
    }

    [HttpPost("{id}/assign")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "ADMINISTRADOR,OPERADOR")]
    public async Task<IActionResult> AssignUnidad(int id, [FromBody] AssignUnidadRequest request)
    {
        var command = new AssignUnidadCommand
        {
            SerId = id,
            ConId = request.ConId,
            VehId = request.VehId
        };
        var result = await _mediator.Send(command);
        if (!result) return NotFound();
        return Ok(new { success = true });
    }

    [HttpPost("{id}/estado")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "ADMINISTRADOR,OPERADOR,CONDUCTOR")]
    public async Task<IActionResult> UpdateEstado(int id, [FromBody] UpdateEstadoRequest request)
    {
        var command = new UpdateServicioEstadoCommand
        {
            SerId = id,
            EstId = request.EstId,
            Observacion = request.Observacion
        };
        var result = await _mediator.Send(command);
        if (!result) return NotFound();
        return Ok(new { success = true });
    }
}

public class AssignUnidadRequest
{
    public int ConId { get; set; }
    public int VehId { get; set; }
}

public class UpdateEstadoRequest
{
    public int EstId { get; set; }
    public string? Observacion { get; set; }
}
