using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBooking.Infrastructure.Identity
{
    public static class AdminSeeder
    {
        public static async Task SeedAdmin(UserManager<ApplicationUser> userManager)
        {
            var adminEmail = "admin@devbooking.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var admin= new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FullName = "System Administrator",
                };
                var result = await userManager.CreateAsync(admin, "Admin@123blablabla");
                if (result.Succeeded)
                {
                    var RoleResult = await userManager.AddToRoleAsync(admin, "Admin");
                    if (!RoleResult.Succeeded)
                    {
                        throw new Exception(
                            string.Join(", ", RoleResult.Errors.Select(e => e.Description))
                        );
                    }
                }
                if (!result.Succeeded)
                {
                    throw new Exception(
                        string.Join(", ", result.Errors.Select(e => e.Description))
                    );
                }
            }
        }
    }
}
