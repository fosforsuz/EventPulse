using EventPulse.Application.Commands.EventParticipant.EventParticipantCreate;
using FluentValidation;

namespace EventPulse.Application.Validation.EventParticipant;

public class CreateEventParticipantCommandValidator : AbstractValidator<CreateEventParticipantCommand>
{
    public CreateEventParticipantCommandValidator()
    {
        RuleFor(x => x.EventId)
            .NotEmpty();

        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}