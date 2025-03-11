namespace LibrariesWeb.Application.Request;

public class UserLoginRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}