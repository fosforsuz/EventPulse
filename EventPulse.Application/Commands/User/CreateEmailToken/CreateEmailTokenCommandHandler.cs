using EventPulse.Infrastructure.Interfaces;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.User.CreateEmailToken;

public class CreateEmailTokenCommandHandler : IRequestHandler<CreateEmailTokenCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmailTokenCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateEmailTokenCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
            return Result.Fail<int>("Invalid user id");

        var user = await _unitOfWork.UserRepository.GetSingleAsync(user => user.Id == request.Id && !user.IsDeleted);

        if (user is null)
            return Result.Fail<int>("User not found");

        user.CreateVerificationToken();

        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(user.Id);
    }
}