namespace EventPulse.Application.Queries.Dtos;

public class EventDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Location { get; set; } = null!;
    public DateTime EventDate { get; set; }
    public string CreatorName { get; set; } = null!;
    public bool IsCompleted { get; set; }
}