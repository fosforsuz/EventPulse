using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.Event.EventCreate;

public abstract record CreateEventCommand(
    string Title,
    string? Description,
    string Location,
    DateTime EventDate,
    int CreatorId)
    : IRequest<Result<int>>;