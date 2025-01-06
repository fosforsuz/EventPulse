using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.User.VerifyEmailToken;

public record CreateEmailTokenCommand(int Id) : IRequest<Result<int>>;