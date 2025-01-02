using EventPulse.Application.Queries.Dtos;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Queries.Event.GetPastEvents;

public record GetPastEventsQuery() : IRequest<Result<List<EventDto>>>;