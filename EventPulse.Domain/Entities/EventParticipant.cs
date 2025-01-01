using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventPulse.Domain.Interfaces;

namespace EventPulse.Domain.Entities;

public class EventParticipant : IBaseEntity
{
    public EventParticipant(int eventId, int userId)
    {
        EventId = eventId;
        UserId = userId;
        JoinedAt = DateTime.Now;
    }

    public int EventId { get; set; }

    public int UserId { get; set; }

    [Column(TypeName = "datetime")] public DateTime? JoinedAt { get; set; }

    [ForeignKey("EventId")]
    [InverseProperty("EventParticipants")]
    public virtual Event Event { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("EventParticipants")]
    public virtual User User { get; set; } = null!;

    [Key] public int Id { get; set; }
}