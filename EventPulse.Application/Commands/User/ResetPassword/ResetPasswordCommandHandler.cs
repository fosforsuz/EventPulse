using EventPulse.Application.Utilities;
using EventPulse.Infrastructure.Interfaces;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.User.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ResetPasswordCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Password))
            return Result.Fail<int>("Password is required");

        var user = await _unitOfWork.UserRepository.GetSingleAsync(@user =>
            @user.PasswordResetToken == request.Token && !@user.IsDeleted);

        if (user is null)
            return Result.Fail<int>("Invalid token");
        
        var now = DateTime.Now;
        
        if (user.PasswordResetExpiresAt is null || user.PasswordResetExpiresAt < now)
            return Result.Fail<int>("Token has expired");

        user.ResetPassword(HashService.HashPassword(request.Password));

        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);
        
        return Result.Ok(user.Id);
    }
}