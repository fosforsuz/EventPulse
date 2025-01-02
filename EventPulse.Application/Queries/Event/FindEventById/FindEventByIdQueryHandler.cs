using EventPulse.Application.Queries.Dtos;
using EventPulse.Application.Queries.FindUserById;
using EventPulse.Infrastructure.Interfaces;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Queries.Event;

public class FindEventByIdQueryHandler : IRequestHandler<FindUserByIdQuery, Result<EventDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public FindEventByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<EventDto>> Handle(FindUserByIdQuery request, CancellationToken cancellationToken)
    {
        var eventObject = await _unitOfWork.EventRepository
            .GetSingleAsync(
                predicate: @event => @event.Id == request.Id,
                selector: @event => new EventDto()
                {
                    Id = @event.Id,
                    Title = @event.Title,
                    Description = @event.Description,
                    Location = @event.Location,
                    EventDate = @event.EventDate,
                    IsCompleted = @event.IsCompleted,
                    CreatorName = @event.Creator.Name
                }
            );

        return eventObject is null ? Result.Fail<EventDto>("Event not found.") : Result.Ok(eventObject);
    }
}