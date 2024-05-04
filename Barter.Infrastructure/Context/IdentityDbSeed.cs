using Barter.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Barter.Infrastructure.Context
{
    public class IdentityDbSeed
    {
        public static async Task SeedDatabaseAsync(UserIdentityDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "User" };
            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            if (!userManager.Users.Any())
            {
                var adminUser = new User { UserName = "admin", Email = "admin@admin.com", EmailConfirmed = true };
                var result = await userManager.CreateAsync(adminUser, "Admin123.?");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
