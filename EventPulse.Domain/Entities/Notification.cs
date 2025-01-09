using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventPulse.Domain.Interfaces;

namespace EventPulse.Domain.Entities;

public sealed class Notification : IBaseEntity
{
    public Notification(int eventId, string message)
    {
        EventId = eventId;
        Message = message;
        SentAt = DateTime.Now;
    }

    public int EventId { get; set; }

    [StringLength(500)] public string Message { get; set; } = null!;

    [Column(TypeName = "datetime")] public DateTime? SentAt { get; set; }

    [Column(TypeName = "datetime")] public DateTime? ReadAt { get; set; }

    public bool? IsRead { get; set; }

    [ForeignKey("EventId")]
    [InverseProperty("Notifications")]
    public Event Event { get; set; } = null!;

    [Key] public int Id { get; set; }

    public void MarkAsRead()
    {
        IsRead = true;
        ReadAt = DateTime.Now;
    }
}