using EventPulse.Application.Queries.Dtos;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Queries.Event.GetPaginatedEvents;

public abstract record GetPaginatedActiveEventsQuery(int PageNumber, int PageSize)
    : IRequest<List<EventDto>>, IRequest<Result<List<EventDto>>>;