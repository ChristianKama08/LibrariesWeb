using AutoMapper;
using FluentValidation;
using LibrariesWeb.Application.Constants;
using LibrariesWeb.Application.Dtos;
using LibrariesWeb.Application.Exceptions;
using LibrariesWeb.Application.Helpers;
using LibrariesWeb.Application.Request;
using LibrariesWeb.Application.Services.Interfaces;
using LibrariesWeb.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace LibrariesWeb.Application.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserService _userService;
    private readonly ILogger<AuthService> _logger;
    private readonly IValidator<UserLoginRequest> _loginValidator;
    private readonly IValidator<UserRegisterRequest> _registerValidator;
    private readonly IMapper _mapper;

    public AuthService(
        UserManager<AppUser> userManager,
        IUserService userService,
        ILogger<AuthService> logger,
        IValidator<UserLoginRequest> loginValidator,
        IValidator<UserRegisterRequest> registerValidator,
        IMapper mapper)
    {
        _userManager = userManager;
        _userService = userService;
        _logger = logger;
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;
        _mapper = mapper;
    }

    public async Task<TokenDto> LoginAsync(UserLoginRequest loginRequest)
    {
        await _loginValidator.ValidateAsync(loginRequest);
        var user = await _userService.GetUserByEmail(loginRequest.Email);
        await _userService.CheckPassword(user, loginRequest.Password);
        await _userService.EnsureEmailConfirmed(user);

        return await _userService.GenerateTokenAsync(user);
    }

    public async Task<TokenDto> RefreshTokenAsync(string token)
    {
        return await _userService.RefreshTokenAsync(token);
    }

    public async Task<TokenDto> RegisterAsync(UserRegisterRequest registerRequest)
    {
        var validationResult = await _registerValidator.ValidateAsync(registerRequest);
        if (!validationResult.IsValid)
        {
            _logger.LogError("Validation failed: {ValidationErrors}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }

        if (await _userManager.IsUserEmailExist(registerRequest.Email))
        {
            _logger.LogError("A user with the email {Email} already exists", registerRequest.Email);
            throw new AlreadyExistsException($"A user with the email {registerRequest.Email} already exists");
        }

        var user = _mapper.Map<AppUser>(registerRequest);
        user.Id = Guid.NewGuid();
        user.EmailConfirmed = true;

        var result = await _userManager.CreateAsync(user, registerRequest.Password);
        result.ThrowExceptionIfResultDoNotSucceed(_logger);
        await _userManager.AddToRoleAsync(user, Roles.User);

        return await _userService.GenerateTokenAsync(user);
    }

    public async Task SignOutAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            _logger.LogError("User with id {UserId} not found", userId);
            throw new NotFoundException("User not found");
        }

        _logger.LogInformation("User with id {UserId} signed out", userId);
    }
}

