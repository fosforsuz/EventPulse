using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.Notification.MarkAsRead;

public abstract record MarkAsReadNotificationCommand(int NotificationId) : IRequest<Result<Unit>>;