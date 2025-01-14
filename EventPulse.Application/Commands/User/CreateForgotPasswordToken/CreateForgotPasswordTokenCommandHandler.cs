using EventPulse.Infrastructure.Interfaces;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.User.CreateForgotPasswordToken;

public class CreateForgotPasswordTokenCommandHandler : IRequestHandler<CreateForgotPasswordTokenCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateForgotPasswordTokenCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateForgotPasswordTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetSingleAsync(user =>
            user.Email.Equals(request.Email) && !user.IsDeleted);

        if (user is null)
            return Result.Fail<int>("User not found");

        user.CreatePasswordResetToken();

        await _unitOfWork.UserRepository.UpdateAsync(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(user.Id);
    }
}