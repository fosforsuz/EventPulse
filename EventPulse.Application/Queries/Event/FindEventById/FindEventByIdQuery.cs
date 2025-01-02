using EventPulse.Application.Queries.Dtos;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Queries.Event;

public record FindEventByIdQuery(int Id) : IRequest<Result<EventDto>>;