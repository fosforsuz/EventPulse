using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.User.CreateForgotPasswordToken;

public record CreateForgotPasswordTokenCommand(string Email) : IRequest<Result<int>>;