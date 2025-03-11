using LibrariesWeb.Persistance.Data;
using LibrariesWeb.Persistance.SeedData;
using Microsoft.EntityFrameworkCore;

namespace LibrariesWeb.API.Extensions;

public static partial class ApplicationDependenciesConfiguration
{
    public async static Task UseMigration(this WebApplication application)
    {
        var serviceScopeFactory = application.Services.GetService<IServiceScopeFactory>();
        using var scope = serviceScopeFactory.CreateScope();

        var handler = scope.ServiceProvider.GetRequiredService<LibraryContext>();
        await handler.Database.MigrateAsync();

        var roleSeed = scope.ServiceProvider.GetRequiredService<SeedRole>();
        await roleSeed.InitializeRolesAsync();

        var adminSeed = scope.ServiceProvider.GetRequiredService<SeedAdmin>();
        await adminSeed.InitializesAdminAsync();
    }
}