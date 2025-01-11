using EventPulse.Application.Commands.Event.EventCreate;
using FluentValidation;

namespace EventPulse.Application.Validation.Event;

public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(4000);

        RuleFor(x => x.Location)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.EventDate)
            .NotEmpty();

        RuleFor(x => x.CreatorId)
            .NotEmpty().WithMessage("Creator is required.");

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("Category is required.")
            .GreaterThan(0).WithMessage("Category is required.");
    }
}