using EventPulse.Api.Models;
using EventPulse.Application.Commands.Event.EventCreate;
using EventPulse.Application.Commands.Event.EventDelete;
using EventPulse.Application.Commands.Event.EventUpdate;
using EventPulse.Application.Queries.Event;
using EventPulse.Application.Queries.Event.GetActiveEvents;
using EventPulse.Application.Queries.Event.GetPastEvents;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventPulse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("active-events")]
    public async Task<IActionResult> GetActiveEvents()
    {
        if (await _mediator.Send(new GetActiveEventsQuery()) is not { } result)
            return BadRequest(ResponseModel.Error("An error occurred while getting the events."));

        if (result.IsSuccess)
            return Ok(ResponseModel.Success(data: result.Value));

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }

    [HttpGet]
    [Route("past-events")]
    public async Task<IActionResult> GetPastEvents()
    {
        if (await _mediator.Send(new GetPastEventsQuery()) is not { } result)
            return BadRequest(ResponseModel.Error("An error occurred while getting the events."));

        if (result.IsSuccess)
            return Ok(ResponseModel.Success(data: result.Value));

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEventById(int id, string message = "")
    {
        if (await _mediator.Send(new FindEventByIdQuery(Id: id)) is not { } result)
            return BadRequest(ResponseModel.Error("An error occurred while getting the event."));

        if (result.IsSuccess)
            return Ok(string.IsNullOrWhiteSpace(message)
                ? ResponseModel.Success(data: result.Value)
                : ResponseModel.Success(message: message, data: result.Value));


        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand request)
    {
        if (await _mediator.Send(request) is not { } result)
            return BadRequest(ResponseModel.Error("An error occurred while creating the event."));

        if (result.IsSuccess)
            return CreatedAtAction(nameof(GetEventById),
                new { id = result.Value, message = "Event created successfully." });

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventCommand request)
    {
        var result = await _mediator.Send(request);

        if (result.IsSuccess)
            return NoContent();

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }

    [HttpPut]
    public async Task<IActionResult> DeleteEvent(DeleteEventCommand request)
    {
        var result = await _mediator.Send(request: request);

        if (result.IsSuccess)
            return NoContent();

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }
}