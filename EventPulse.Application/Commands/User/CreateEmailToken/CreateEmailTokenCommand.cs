using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.User.CreateEmailToken;

public abstract record CreateEmailTokenCommand(int Id) : IRequest<Result<int>>;