namespace LibrariesWeb.Domain.Entities;

public class RefreshToken 
{
    public Guid Id { get; init; }
    public required string Token { get; set; }

    public Guid UserId { get; set; }

    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

    public DateTime ExpiryDate { get; set; }

    public bool IsRevoked { get; set; } = false;
}
