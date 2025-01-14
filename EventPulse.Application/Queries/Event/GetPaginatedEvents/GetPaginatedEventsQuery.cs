using EventPulse.Application.Queries.Dtos;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Queries.Event.GetPaginatedEvents;

public class GetPaginatedEventsQuery : IRequest<List<EventDto>>, IRequest<Result<List<EventDto>>>
{
    public GetPaginatedEventsQuery(int pageNumber = 0, int pageSize = 12, bool isCompleted = false)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        IsCompleted = isCompleted;
    }

    public int PageNumber { get; }
    public int PageSize { get; } = 12;
    public bool IsCompleted { get; private set; }

    public void GetCompletedEvents()
    {
        IsCompleted = true;
    }

    public void GetUncompletedEvents()
    {
        IsCompleted = false;
    }
}