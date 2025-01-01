using EventPulse.Infrastructure.Interfaces;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.User.UserDelete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
            return Result.Fail<int>("Invalid Id");

        var user = await _unitOfWork.UserRepository.FindByIdAsync(request.Id);

        if (user == null)
            return Result.Fail<int>("User not found");

        user.Delete();

        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(user.Id);
    }
}