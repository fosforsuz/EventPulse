using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventPulse.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventPulse.Domain.Entities;

/// <summary>
/// Represents a user entity within the system, including properties for authentication,
/// verification, and relationship mapping to associated events and participants.
/// </summary>
[Index("Email", Name = "UQ__Users__A9D105341A680F55", IsUnique = true)]
public class User : IBaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class with the provided details.
    /// </summary>
    /// <param name="name">The name of the user.</param>
    /// <param name="email">The email address of the user.</param>
    /// <param name="passwordHash">The hashed password of the user.</param>
    /// <param name="role">The role assigned to the user.</param>
    public User(string name, string email, string passwordHash, string role)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = DateTime.Now;
        IsEmailVerified = false;
    }

    [StringLength(100)] public string Name { get; set; }

    [Required, EmailAddress, StringLength(100)]
    public string Email { get; set; }

    [Required, StringLength(100, MinimumLength = 6)]
    public string PasswordHash { get; set; }

    [StringLength(50)] public string Role { get; set; }

    [Column(TypeName = "datetime")] public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")] public DateTime? UpdateAt { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsEmailVerified { get; set; }

    public DateTime? VerifiedAt { get; set; }

    public Guid VerifiedToken { get; set; }

    public DateTime? VerificationExpiresAt { get; set; }

    public Guid PasswordResetToken { get; set; }

    public DateTime? PasswordResetExpiresAt { get; set; }

    public DateTime? PasswordResetAt { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();

    [InverseProperty("Creator")] public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    [Key] public int Id { get; set; }

    /// <summary>
    /// Updates the user's name and password hash, and updates the last modified timestamp.
    /// </summary>
    /// <param name="name">New name of the user.</param>
    /// <param name="passwordHash">New hashed password of the user.</param>
    public void Update(string name, string passwordHash)
    {
        Name = name;
        PasswordHash = passwordHash;
        UpdateAt = DateTime.Now;
    }

    /// <summary>
    /// Creates a new email verification token with a set expiration time.
    /// </summary>
    public void CreateVerificationToken()
    {
        VerifiedToken = Guid.NewGuid();
        VerificationExpiresAt = DateTime.Now.AddMinutes(5);
    }

    /// <summary>
    /// Marks the user's email as verified and sets the verification timestamp.
    /// </summary>
    public void VerifyEmail()
    {
        IsEmailVerified = true;
        VerifiedAt = DateTime.Now;
        VerifiedToken = Guid.Empty;
        VerificationExpiresAt = null;
    }

    /// <summary>
    /// Resets the email verification token and clears its expiration time.
    /// </summary>
    public void ResetVerificationToken()
    {
        VerifiedToken = Guid.Empty;
        VerificationExpiresAt = null;
    }

    /// <summary>
    /// Generates a password reset token with an expiration time.
    /// </summary>
    public void CreatePasswordResetToken()
    {
        PasswordResetToken = Guid.NewGuid();
        PasswordResetExpiresAt = DateTime.Now.AddMinutes(5);
    }

    /// <summary>
    /// Resets the user's password and logs the reset timestamp.
    /// </summary>
    /// <param name="passwordHash">The new hashed password.</param>
    public void ResetPassword(string passwordHash)
    {
        PasswordHash = passwordHash;
        PasswordResetAt = DateTime.Now;
    }

    /// <summary>
    /// Resets the password reset token and clears its expiration time.
    /// </summary>
    public void ResetPasswordToken()
    {
        PasswordResetToken = Guid.Empty;
        PasswordResetExpiresAt = null;
    }

    /// <summary>
    /// Marks the user as deleted and updates the last modified timestamp.
    /// </summary>
    public void Delete()
    {
        IsDeleted = true;
        UpdateAt = DateTime.Now;
    }

    /// <summary>
    /// Authenticates the user by verifying the provided password against the stored hash.
    /// </summary>
    /// <param name="password">The plain-text password to verify.</param>
    /// <returns>True if the password is correct; otherwise, false.</returns>
    public bool Authenticate(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }

    /// <summary>
    /// Sets a new password by hashing it.
    /// </summary>
    /// <param name="password">The plain-text password to hash and set.</param>
    protected void SetPassword(string password)
    {
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
    }
}