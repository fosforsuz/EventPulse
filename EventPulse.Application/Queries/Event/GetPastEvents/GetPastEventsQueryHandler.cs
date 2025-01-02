using EventPulse.Application.Queries.Dtos;
using EventPulse.Infrastructure.Interfaces;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Queries.Event.GetPastEvents;

public class GetPastEventsQueryHandler : IRequestHandler<GetPastEventsQuery, Result<List<EventDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPastEventsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<EventDto>>> Handle(GetPastEventsQuery request, CancellationToken cancellationToken)
    {
        var lastSixMonths = DateTime.Now.AddMonths(-6);

        var pastEvents = await _unitOfWork.EventRepository
            .GetAsync(
                predicate: @event =>
                    @event.EventDate < DateTime.Now && @event.EventDate > lastSixMonths && @event.IsCompleted &&
                    @event.IsDeleted,
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

        return Result.Ok(pastEvents);
    }
}