using EventPulse.Application.Utilities;
using EventPulse.Infrastructure.Interfaces;
using FluentResults;
using FluentValidation;
using MediatR;

namespace EventPulse.Application.Commands.User.UserUpdate;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateUserCommand> _validator;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IValidator<UpdateUserCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<int>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Fail<int>("Validation failed")
                .WithErrors(validationResult.Errors.Select(error => error.ErrorMessage));

        var user = await _unitOfWork.UserRepository.FindByIdAsync(request.Id);

        if (user is null)
            return Result.Fail<int>("User not found");

        user.Update(request.Name, HashService.HashPassword(request.Password));

        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(user.Id);
    }
}