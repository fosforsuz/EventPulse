using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventPulse.Domain.Interfaces;

namespace EventPulse.Domain.Entities;

public sealed class Event : IBaseEntity
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

    [StringLength(200)] public string Title { get; set; }

    public string? Description { get; set; }

    [StringLength(200)] public string Location { get; set; }
    
    [StringLength(200)] public string? EventPhotoPath { get; set; }

    [Column(TypeName = "datetime")] public DateTime EventDate { get; set; }

    public int CreatorId { get; set; }

    public int CategoryId { get; set; }

    [Column(TypeName = "datetime")] public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")] public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }
    public bool IsCompleted { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("Events")]
    public User Creator { get; set; } = null!;

    [ForeignKey("CategoryId")]
    [InverseProperty("Events")]
    public Category Category { get; set; } = null!;
    

    [InverseProperty("Event")]
    public ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();

    [InverseProperty("Event")]
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

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