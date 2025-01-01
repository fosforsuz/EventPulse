using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.User.UserCreate;

public abstract record CreateUserCommand(string Name, string Email, string Password, string Role)
    : IRequest<int>, IRequest<Result<int>>;