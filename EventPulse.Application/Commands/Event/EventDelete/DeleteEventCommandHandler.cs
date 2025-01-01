using EventPulse.Infrastructure.Interfaces;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.Event.EventDelete;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEventCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
            return Result.Fail<int>("Id is required");

        var eventEntity = await _unitOfWork.EventRepository.FindByIdAsync(request.Id);

        if (eventEntity == null)
            return Result.Fail<int>("Event not found");

        eventEntity.Delete();

        await _unitOfWork.EventRepository.UpdateAsync(eventEntity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(eventEntity.Id);
    }
}