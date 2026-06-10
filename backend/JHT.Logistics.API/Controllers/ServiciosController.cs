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
}
