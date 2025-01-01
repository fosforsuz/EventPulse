using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.EventParticipant.EventParticipantCreate;

public record CreateEventParticipantCommand(int EventId, int UserId) : IRequest<Result<int>>;