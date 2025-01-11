using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.Event.EventUpdate;

public record UpdateEventCommand(int Id, string Title, string? Description, string Location, DateTime EventDate, int CategoryId)
    : IRequest<Result<int>>;