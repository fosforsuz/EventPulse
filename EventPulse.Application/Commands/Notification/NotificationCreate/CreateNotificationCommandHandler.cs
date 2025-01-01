using EventPulse.Infrastructure.Interfaces;
using FluentResults;
using FluentValidation;
using MediatR;

namespace EventPulse.Application.Commands.Notification.NotificationCreate;

public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateNotificationCommand> _validator;

    public CreateNotificationCommandHandler(IUnitOfWork unitOfWork, IValidator<CreateNotificationCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<int>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Fail<int>("Validation Failed")
                .WithErrors(validationResult.Errors.Select(error => error.ErrorMessage));

        var notification = new Domain.Entities.Notification(request.EventId, request.Message);
        await _unitOfWork.NotificationRepository.AddAsync(notification);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(notification.Id);
    }
}