using System;
using System.Threading.Tasks;
using Lun2Code.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Lun2Code
{
    public static class Initializer
    {
        public static async Task Init(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            var adminEmail = configuration.GetSection("Admin")["Email"];
            var adminPassword = configuration.GetSection("Admin")["Password"];

            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (await roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            var admin = new User
            {
                UserName = adminEmail,
                Email = adminEmail,
                Name = "Магнит",
                Surname = "Maгнитович",
                Country = "Беларусь",
                City = "Минск"
            };

            var result = await userManager.CreateAsync(admin, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
                await userManager.AddToRoleAsync(admin, "User");
            }

        }

    }
        
}