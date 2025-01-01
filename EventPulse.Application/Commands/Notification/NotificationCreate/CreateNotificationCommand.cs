using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.Notification.NotificationCreate;

public abstract record CreateNotificationCommand(int EventId, string Message) : IRequest<Result<int>>;