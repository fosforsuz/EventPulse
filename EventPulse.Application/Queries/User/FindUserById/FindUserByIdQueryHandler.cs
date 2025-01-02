using EventPulse.Application.Queries.Dtos;
using EventPulse.Infrastructure.Interfaces;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Queries.FindUserById;

public class FindUserByIdQueryHandler : IRequestHandler<FindUserByIdQuery, Result<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public FindUserByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDto>> Handle(FindUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.FindByIdAsync(request.Id);

        if (user is null)
            return Result.Fail<UserDto>("User not found.");

        var userDto = new UserDto()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            ModifiedAt = user.UpdateAt
        };

        return Result.Ok(userDto);
    }
}