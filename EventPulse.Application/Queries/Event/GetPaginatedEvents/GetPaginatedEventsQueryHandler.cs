using EventPulse.Application.Queries.Dtos;
using EventPulse.Infrastructure.Interfaces;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Queries.Event.GetPaginatedEvents;

public class
    GetPaginatedEventsQueryHandler : IRequestHandler<GetPaginatedEventsQuery, Result<List<EventDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPaginatedEventsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<EventDto>>> Handle(GetPaginatedEventsQuery request,
        CancellationToken cancellationToken)
    {
        var events = await _unitOfWork.EventRepository.GetAsync(
            predicate: @event => @event.IsCompleted == request.IsCompleted && !@event.IsDeleted,
            selector: @event => new EventDto()
            {
                Id = @event.Id,
                Title = @event.Title,
                Description = @event.Description,
                CreatorName = @event.Creator.Name,
                EventDate = @event.EventDate,
                Location = @event.Location,
                IsCompleted = @event.IsCompleted
            },
            skip: 0,
            take: 12,
            tracking: false
        );

        return Result.Ok(events);
    }
}