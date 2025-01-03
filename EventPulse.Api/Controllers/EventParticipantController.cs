using EventPulse.Api.Models;
using EventPulse.Application.Commands.EventParticipant.EventParticipantCreate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventPulse.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventParticipantController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventParticipantController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEventParticipant([FromBody] CreateEventParticipantCommand request)
    {
        if (await _mediator.Send(request) is not { } result)
            return BadRequest(ResponseModel.Error("Failed to create event participant."));

        if (result.IsSuccess)
            return Ok(ResponseModel.Success("Your participation has been registered successfully."));

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }
}