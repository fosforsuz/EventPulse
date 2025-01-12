using EventPulse.Application.Queries.Dtos;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Queries.Event.GetPaginatedEvents;

public class GetPaginatedEventsQuery : IRequest<List<EventDto>>, IRequest<Result<List<EventDto>>>
{
    public int PageNumber { get; } = 0;
    public int PageSize { get; } = 12;
    public bool IsCompleted { get; private set; } = false;

    public GetPaginatedEventsQuery(int pageNumber = 0, int pageSize = 12, bool isCompleted = false)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        IsCompleted = isCompleted;
    }

    public void GetCompletedEvents()
    {
        IsCompleted = true;
    }

    public void GetUncompletedEvents()
    {
        IsCompleted = false;
    }

}