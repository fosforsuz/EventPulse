using EventPulse.Infrastructure.Interfaces;
using FluentResults;
using FluentValidation;
using MediatR;

namespace EventPulse.Application.Commands.EventParticipant.EventParticipantCreate;

public class CreateEventParticipantCommandHandler : IRequestHandler<CreateEventParticipantCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateEventParticipantCommand> _validator;

    public CreateEventParticipantCommandHandler(IUnitOfWork unitOfWork,
        IValidator<CreateEventParticipantCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<int>> Handle(CreateEventParticipantCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Fail<int>("Validation Failed")
                .WithErrors(validationResult.Errors.Select(error => error.ErrorMessage));

        var isUserAlreadyParticipant = await _unitOfWork.EventParticipantRepository.AnyAsync(
            participant => participant.EventId == request.EventId && participant.UserId == request.UserId);

        if (isUserAlreadyParticipant)
            return Result.Fail<int>("User is already a participant of this event");

        var eventParticipant = new Domain.Entities.EventParticipant(request.EventId, request.UserId);

        await _unitOfWork.EventParticipantRepository.AddAsync(eventParticipant);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(eventParticipant.Id);
    }
}