using EventPulse.Infrastructure.Modals;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.Authenticate;

public abstract record AuthenticateCommand(string Email, string Password) : IRequest<Result<AuthanticateResponse>>;