using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventPulse.Domain.Interfaces;

namespace EventPulse.Domain.Entities;

public class Event : IBaseEntity
{
    public Event(string title, string? description, string location, DateTime eventDate, int creatorId, bool isDeleted,
        bool isCompleted)
    {
        Title = title;
        Description = description;
        Location = location;
        EventDate = eventDate;
        CreatorId = creatorId;
        IsDeleted = isDeleted;
        IsCompleted = isCompleted;
        CreatedAt = DateTime.Now;
    }

    [StringLength(200)] public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [StringLength(200)] public string Location { get; set; } = null!;

    [Column(TypeName = "datetime")] public DateTime EventDate { get; set; }

    public int CreatorId { get; set; }

    [Column(TypeName = "datetime")] public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")] public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }
    public bool IsCompleted { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("Events")]
    public virtual User Creator { get; set; } = null!;

    [InverseProperty("Event")]
    public virtual ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();

    [InverseProperty("Event")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [Key] public int Id { get; set; }

    public void Update(string title, string? description, string location, DateTime eventDate)
    {
        Title = title;
        Description = description;
        Location = location;
        EventDate = eventDate;
        UpdatedAt = DateTime.Now;
    }

    public void Delete()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.Now;
    }

    public void Complete()
    {
        IsCompleted = true;
        UpdatedAt = DateTime.Now;
    }
}