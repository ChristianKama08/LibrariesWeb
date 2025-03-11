using LibrariesWeb.Application.Dtos;
using LibrariesWeb.Domain.Entities;

namespace LibrariesWeb.Application.Services.Interfaces;

public interface IUserService
{
    Task<TokenDto> GenerateTokenAsync(AppUser user);
    Task<TokenDto> RefreshTokenAsync(string token);
    Task RevokeRefreshTokensAsync(Guid userId);
    Task<AppUser> GetUserByEmail(string email);
    Task CheckPassword(AppUser user, string password);
    Task EnsureEmailConfirmed(AppUser user);
    Task ValidateRequest<T>(T request, CancellationToken cancellationToken) where T : class;
}
