using EventPulse.Application.Commands.User.UserUpdate;
using FluentValidation;

namespace EventPulse.Application.Validation.Event;

public class UpdateEventCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateEventCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(255);
    }
}