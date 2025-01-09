using EventPulse.Domain.Interfaces;

namespace EventPulse.Application.Queries.Dtos;

public class UserDto : IBaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public DateTime? ModifiedAt { get; set; }
}