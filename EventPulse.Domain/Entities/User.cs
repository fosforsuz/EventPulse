using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventPulse.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventPulse.Domain.Entities;

[Index("Email", Name = "UQ__Users__A9D105341A680F55", IsUnique = true)]
public class User : IBaseEntity
{
    public User(string name, string email, string passwordHash, string role)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = DateTime.Now;
    }

    [StringLength(100)] public string Name { get; set; }

    [StringLength(100)] public string Email { get; set; }

    [StringLength(255)] public string PasswordHash { get; set; }

    [StringLength(50)] public string Role { get; set; }

    [Column(TypeName = "datetime")] public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")] public DateTime? UpdateAt { get; set; }

    public bool IsDeleted { get; set; }


    [InverseProperty("User")]
    public virtual ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();

    [InverseProperty("Creator")] public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    [Key] public int Id { get; set; }

    public void Update(string name, string passwordHash)
    {
        Name = name;
        PasswordHash = passwordHash;
        UpdateAt = DateTime.Now;
    }

    public void Delete()
    {
        IsDeleted = true;
        UpdateAt = DateTime.Now;
    }

    public bool Authenticate(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }

    protected void SetPassword(string password)
    {
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
    }
}