using LibrariesWeb.Application.Constants;
using LibrariesWeb.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibrariesWeb.Persistance.SeedData;

public class SeedAdmin
{
    public const string ADMIN_EMAIL = "christian@gmail.com";
    private readonly UserManager<AppUser> _userManager;

    public SeedAdmin(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task InitializesAdminAsync()
    {
        string firstName = "Christian";
        string lastName = "Kama";
        string userName = "KamaJean23";


        string password = "Kama@christian23";
        var adminUser = await _userManager.FindByNameAsync(firstName);

        if (adminUser == null)
        {
            adminUser = new AppUser
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Email = ADMIN_EMAIL,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(adminUser, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, Roles.Admin);
            }
        }
    }
}
