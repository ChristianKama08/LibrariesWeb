﻿using LibrariesWeb.Domain.Entities;
using LibrariesWeb.Persistance.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibrariesWeb.Persistance.Extensions;

public static class ApplicationDependenciesConfiguration
{
    public static IServiceCollection AddIdentityDatabase(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
    {

        services
            .AddDbContext<LibraryContext>(options)
            .AddIdentity<AppUser, IdentityRole<Guid>>(optionsIdentity =>
            {
                optionsIdentity.User.RequireUniqueEmail = true;
                optionsIdentity.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+& ";
                optionsIdentity.Password.RequireNonAlphanumeric = false;
                optionsIdentity.Password.RequireLowercase = false;
                optionsIdentity.Password.RequireUppercase = false;
                optionsIdentity.Password.RequireDigit = false;
                optionsIdentity.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                optionsIdentity.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
            })
            .AddEntityFrameworkStores<LibraryContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
