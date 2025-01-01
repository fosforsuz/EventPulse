using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.User.UserUpdate;

public abstract record UpdateUserCommand(int Id, string Name, string Password) : IRequest<Result<int>>;