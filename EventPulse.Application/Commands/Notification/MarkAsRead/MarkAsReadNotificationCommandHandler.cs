using EventPulse.Infrastructure.Interfaces;
using FluentResults;
using FluentValidation;
using MediatR;

namespace EventPulse.Application.Commands.Notification.MarkAsRead;

public class MarkAsReadNotificationCommandHandler : IRequestHandler<MarkAsReadNotificationCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<MarkAsReadNotificationCommand> _validator;

    public MarkAsReadNotificationCommandHandler(IUnitOfWork unitOfWork,
        IValidator<MarkAsReadNotificationCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Unit>> Handle(MarkAsReadNotificationCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Fail<Unit>("Validation failed")
                .WithErrors(validationResult.Errors.Select(error => error.ErrorMessage));

        var notification = await _unitOfWork.NotificationRepository.FindByIdAsync(request.NotificationId);

        if (notification is null)
            return Result.Fail<Unit>("Notification not found");

        notification.MarkAsRead();

        await _unitOfWork.NotificationRepository.UpdateAsync(notification);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(Unit.Value);
    }
}