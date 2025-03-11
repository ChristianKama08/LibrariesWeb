using Microsoft.AspNetCore.Identity;

namespace LibrariesWeb.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = []!;
}