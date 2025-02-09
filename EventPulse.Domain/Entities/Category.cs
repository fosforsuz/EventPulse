using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventPulse.Domain.Interfaces;

namespace EventPulse.Domain.Entities;

public sealed class Category : IBaseEntity
{
    public Category(string name, string? description)
    {
        Name = name;
        Description = description;
        CreatedAt = DateTime.Now;
    }

    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }

    [InverseProperty("Category")] public ICollection<Event> Events { get; set; } = new List<Event>();
    [Key] public int Id { get; set; }

    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
        ModifiedAt = DateTime.Now;
    }
}