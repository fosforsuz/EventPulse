using EventPulse.Application.Queries.Dtos;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Queries.FindUserById;

public record FindUserByIdQuery(int Id) : IRequest<UserDto>, IRequest<Result<UserDto>>;