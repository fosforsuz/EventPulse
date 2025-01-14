using FluentResults;
using MediatR;

namespace EventPulse.Application.Commands.Event.EventCreate;

public class CreateEventCommand : IRequest<Result<int>>
{
    public string Title { get; } = null!;
    public string? Description { get; }
    public string Location { get; } = null!;
    public DateTime EventDate { get; }
    public int CreatorId { get; }
    public int CategoryId { get; }
    public Stream? ImageStream { get; private set; }


    public void SetImageStream(Stream imageStream)
    {
        ImageStream = imageStream;
    }
}