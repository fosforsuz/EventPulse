using EventPulse.Application.Commands.User.UserUpdate;
using FluentValidation;

namespace EventPulse.Application.Validation.User;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(255);
    }
}