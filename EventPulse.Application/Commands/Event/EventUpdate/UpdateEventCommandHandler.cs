using EventPulse.Infrastructure.Interfaces;
using FluentResults;
using FluentValidation;
using MediatR;

namespace EventPulse.Application.Commands.Event.EventUpdate;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateEventCommand> _validator;

    public UpdateEventCommandHandler(IUnitOfWork unitOfWork, IValidator<UpdateEventCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<int>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            return Result.Fail<int>("Validation Failed")
                .WithErrors(validatorResult.Errors.Select(error => error.ErrorMessage));

        var eventToUpdate = await _unitOfWork.EventRepository.FindByIdAsync(request.Id);

        if (eventToUpdate is null)
            return Result.Fail<int>("Event not found");

        eventToUpdate.Update(request.Title, request.Description, request.Location, request.EventDate);

        await _unitOfWork.EventRepository.UpdateAsync(eventToUpdate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(eventToUpdate.Id);
    }
}