using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.User.UserDelete;

public record DeleteUserCommand(int Id) : IRequest<Result<int>>;