using Microsoft.AspNetCore.Identity;

namespace DevBooking.Infrastructure.Identity;

public static class RoleSeeder
{
    public static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager)
    {
        string[] roles =
        {
            "Admin",
            "Client",
            "Freelancer"
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = role,
                    NormalizedName = role.ToUpper()
                });
            }
        }
    }
}