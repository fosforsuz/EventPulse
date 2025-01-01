using EventPulse.Application.Commands.User.UserCreate;
using FluentValidation;

namespace EventPulse.Application.Validation.Event;

public class CreateEventCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(100);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Role).NotEmpty().MaximumLength(50);
    }
}