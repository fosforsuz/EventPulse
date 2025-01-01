using EventPulse.Application.Commands.Notification.MarkAsRead;
using FluentValidation;

namespace EventPulse.Application.Validation.Notification;

public class MarkAsReadNotificationCommandValidator : AbstractValidator<MarkAsReadNotificationCommand>
{
    public MarkAsReadNotificationCommandValidator()
    {
        RuleFor(x => x.NotificationId).GreaterThan(0);
    }
}