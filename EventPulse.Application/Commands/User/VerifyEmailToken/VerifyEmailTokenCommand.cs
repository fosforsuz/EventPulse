using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.User.VerifyEmailToken;

public record VerifyEmailTokenCommand(Guid Token) : IRequest<Result<int>>;