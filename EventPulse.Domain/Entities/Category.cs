using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPulse.Domain.Entities;

public sealed class Category
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }

    [InverseProperty("Category")] public ICollection<Event> Events { get; set; } = new List<Event>();

    public Category(string name, string? description)
    {
        Name = name;
        Description = description;
        CreatedAt = DateTime.Now;
    }

    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
        ModifiedAt = DateTime.Now;
    }
}