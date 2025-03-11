using LibrariesWeb.Application.Dtos;
using LibrariesWeb.Application.Request;

namespace LibrariesWeb.Application.Services.Interfaces;

public interface IAuthService
{
    Task<TokenDto> LoginAsync(UserLoginRequest loginRequest);
    Task<TokenDto> RegisterAsync(UserRegisterRequest registerRequest);
    Task<TokenDto> RefreshTokenAsync(string token);
    Task SignOutAsync(Guid userId);
}