using EventPulse.Infrastructure.Interfaces;
using EventPulse.Infrastructure.Modals;
using EventPulse.Infrastructure.Security;
using FluentResults;
using FluentValidation;
using MediatR;

namespace EventPulse.Application.Commands.Authenticate;

public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, Result<AuthanticateResponse>>
{
    private readonly IJwtService _jwtService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AuthenticateCommand> _validator;

    public AuthenticateCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService,
        IValidator<AuthenticateCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _validator = validator;
    }

    public async Task<Result<AuthanticateResponse>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Fail<AuthanticateResponse>("Validation failed.")
                .WithErrors(validationResult.Errors.Select(error => error.ErrorMessage));

        var user = await _unitOfWork.UserRepository.GetSingleAsync(user =>
            user.Email == request.Email && !user.IsDeleted);

        if (user == null || !user.Authenticate(request.Password))
            return Result.Fail<AuthanticateResponse>("Invalid email or password.");


        var token = _jwtService.GenerateToken(user.Id, user.Role, user.Name, user.Email);
        return Result.Ok(token);
    }
}