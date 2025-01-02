using EventPulse.Application.Queries.Dtos;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Queries.Event.GetActiveEvents;

public record GetActiveEventsQuery() : IRequest<Result<List<EventDto>>>;