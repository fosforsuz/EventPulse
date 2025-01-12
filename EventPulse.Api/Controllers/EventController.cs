using EventPulse.Api.Models;
using EventPulse.Application.Commands.Event.EventCreate;
using EventPulse.Application.Commands.Event.EventDelete;
using EventPulse.Application.Commands.Event.EventUpdate;
using EventPulse.Application.Queries.Dtos;
using EventPulse.Application.Queries.Event;
using EventPulse.Application.Queries.Event.GetPaginatedEvents;
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
    public async Task<IActionResult> GetActiveEvents([FromQuery] GetPaginatedEventsQuery request)
    {
        request.GetUncompletedEvents();

        if (await _mediator.Send(request) is not Result<List<EventDto>> result)
            return BadRequest(ResponseModel.Error("An error occurred while getting the events."));

        if (result.IsSuccess)
            return Ok(ResponseModel.Success(result.Value));

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }

    [HttpGet]
    [Route("past-events")]
    public async Task<IActionResult> GetPastEvents([FromQuery] GetPaginatedEventsQuery request)
    {
        request.GetCompletedEvents();

        if (await _mediator.Send(request) is not Result<List<EventDto>> result)
            return BadRequest(ResponseModel.Error("An error occurred while getting the events."));

        if (result.IsSuccess)
            return Ok(ResponseModel.Success(result.Value));

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEventById(int id, string message = "")
    {
        if (await _mediator.Send(new FindEventByIdQuery(id)) is not { } result)
            return BadRequest(ResponseModel.Error("An error occurred while getting the event."));

        if (result.IsSuccess)
            return Ok(string.IsNullOrWhiteSpace(message)
                ? ResponseModel.Success(result.Value)
                : ResponseModel.Success(message: message, data: result.Value));


        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand request, [FromBody] IFormFile eventImage)
    {
        // 1. Validasyonlar
        var validationResult = ValidateEventImage(eventImage);
        if (!validationResult.IsSuccess)
            return BadRequest(validationResult.Message);

        // 2. Görseli işle ve komuta aktar
        request.SetImageStream(eventImage.OpenReadStream());

        // 3. Komutu işleyip sonucu kontrol et
        var result = await _mediator.Send(request);
        if (result is null || !result.IsSuccess)
            return BadRequest(ResponseModel.Error(result?.Errors?.FirstOrDefault()?.Message ?? "An error occurred while creating the event."));

        // 4. Başarılı yanıt döndür
        return CreatedAtAction(nameof(GetEventById), new { id = result.Value }, ResponseModel.Success("Event created successfully."));
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
        var result = await _mediator.Send(request);

        if (result.IsSuccess)
            return NoContent();

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }

    private (bool IsSuccess, string Message) ValidateEventImage(IFormFile eventImage)
    {
        if (eventImage is null || eventImage.Length == 0)
            return (false, "Event image is required.");

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var fileExtension = Path.GetExtension(eventImage.FileName).ToLower();

        if (!allowedExtensions.Contains(fileExtension))
            return (false, "Unsupported file type.");

        if (eventImage.Length > 2 * 1024 * 1024)
            return (false, "File size cannot exceed 2MB.");

        return (true, string.Empty);
    }

}