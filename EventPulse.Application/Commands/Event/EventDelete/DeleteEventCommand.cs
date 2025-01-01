using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.Event.EventDelete;

public record DeleteEventCommand(int Id) : IRequest<Result<int>>;