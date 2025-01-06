using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.User.ResetPassword;

public record ResetPasswordCommand(Guid Token, string Password) : IRequest<Result<int>>;