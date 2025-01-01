using EventPulse.Application.Commands.Notification.NotificationCreate;
using FluentValidation;

namespace EventPulse.Application.Validation.Notification;

public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
{
    public CreateNotificationCommandValidator()
    {
        RuleFor(x => x.EventId).GreaterThan(0);
        RuleFor(x => x.Message).NotEmpty();
    }
}