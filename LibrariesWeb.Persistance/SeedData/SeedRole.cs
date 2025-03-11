using LibrariesWeb.Application.Constants;
using Microsoft.AspNetCore.Identity;

namespace LibrariesWeb.Persistance.SeedData;

public class SeedRole
{
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public SeedRole(RoleManager<IdentityRole<Guid>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task InitializeRolesAsync()
    {
        string[] roleNames = [Roles.Admin, Roles.User];

        foreach (var roleName in roleNames)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (roleExist)
            {
                continue;
            }

            var role = new IdentityRole<Guid>(roleName);
            await _roleManager.CreateAsync(role);
        }
    }
}
