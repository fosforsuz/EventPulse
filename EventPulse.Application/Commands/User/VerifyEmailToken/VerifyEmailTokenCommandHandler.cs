using EventPulse.Infrastructure.Interfaces;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.User.VerifyEmailToken;

public class VerifyEmailTokenCommandHandler : IRequestHandler<VerifyEmailTokenCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public VerifyEmailTokenCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(VerifyEmailTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetSingleAsync(user =>
            user.VerifiedToken.Equals(request.Token) && !user.IsDeleted);

        if (user is null)
            return Result.Fail<int>("Invalid token");

        var now = DateTime.Now;

        if (user.VerificationExpiresAt is null || user.VerificationExpiresAt < now)
            return Result.Fail<int>("Token has expired");

        user.VerifyEmail();

        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(user.Id);
    }
}