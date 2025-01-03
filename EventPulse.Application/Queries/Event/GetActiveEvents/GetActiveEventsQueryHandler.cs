using EventPulse.Application.Queries.Dtos;
using EventPulse.Infrastructure.Interfaces;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Queries.Event.GetActiveEvents;

public class GetActiveEventsQueryHandler : IRequestHandler<GetActiveEventsQuery, Result<List<EventDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetActiveEventsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<EventDto>>> Handle(GetActiveEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _unitOfWork.EventRepository
            .GetAsync(
                @event => @event.EventDate >= DateTime.Now && !@event.IsDeleted && !@event.IsCompleted,
                @event => new EventDto
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

        return Result.Ok(events);
    }
}