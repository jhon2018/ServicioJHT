using JHT.Logistics.Application.Features.Servicios.DTOs;
using JHT.Logistics.Application.Features.Servicios.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JHT.Logistics.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous] // Endpoint público
public class TrackingController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrackingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{token}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrackingDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTracking(string token)
    {
        var result = await _mediator.Send(new GetTrackingByTokenQuery { Token = token });
        
        if (result == null)
            return NotFound(new { message = "Link de tracking inválido, expirado o no encontrado." });

        return Ok(result);
    }
}
