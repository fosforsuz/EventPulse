using EventPulse.Application.Commands.Authenticate;
using FluentValidation;

namespace EventPulse.Application.Validation.Authentication;

public class AuthenticationCommandValidator : AbstractValidator<AuthenticateCommand>
{
    public AuthenticationCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email is not valid.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}