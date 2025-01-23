using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Petlover.Enums;
using Petlover.Models;

namespace Petlover.Extensions;

public static class SeedExtension
{
    public static void UseUserSeed(this IApplicationBuilder app)
    {
        using(var scope = app.ApplicationServices.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            CreateRoles(roleManager).Wait();
            CreateUsers(userManager).Wait();
        }
    }

    private static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
    {
        if(!await roleManager.Roles.AnyAsync()){
            foreach(Roles item in Enum.GetValues(typeof(Roles)))
            {
                await roleManager.CreateAsync(new IdentityRole(item.GetRole()));
            }
        }
    }

    private static async Task CreateUsers(UserManager<User> userManager) 
    {

        if (!await userManager.Users.AnyAsync(u => u.NormalizedUserName == "ADMIN"))
        {
            User user = new User();
            user.UserName = "admin";
            user.Email = "admin@gmail.com";
            user.FullName = "admin";
            user.EmailConfirmed = true;
            string role = nameof(Roles.Admin);
            await userManager.CreateAsync(user,"admin123");
            await userManager.AddToRoleAsync(user,role);
        }
    }
}
