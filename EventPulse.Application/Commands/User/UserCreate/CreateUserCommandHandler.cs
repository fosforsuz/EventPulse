using EventPulse.Application.Utilities;
using EventPulse.Infrastructure.Interfaces;
using FluentResults;
using FluentValidation;
using MediatR;

namespace EventPulse.Application.Commands.User.UserCreate;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateUserCommand> _validator;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IValidator<CreateUserCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Fail<int>("Validation failed")
                .WithErrors(validationResult.Errors.Select(error => error.ErrorMessage));

        var isEmailUnique = await _unitOfWork.UserRepository.AnyAsync(user => user.Email.Equals(request.Email));
        if (isEmailUnique) return Result.Fail<int>("Email is already taken");

        var user = new Domain.Entities.User(request.Name, request.Email, HashService.HashPassword(request.Password),
            request.Role);

        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(user.Id);
    }
}