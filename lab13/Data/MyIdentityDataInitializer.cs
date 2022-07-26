﻿using Microsoft.AspNetCore.Identity;

namespace lab10.Data
{
    public class MyIdentityDataInitializer
    {
        public static void SeedData(WebApplicationBuilder app)
        {
            var scope = app.Services.BuildServiceProvider().CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        // name - poprawny adres email
        // password - min 8 znaków, mała i duża litera, cyfra i znak specjalny
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin",
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Dean").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Dean",
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Normal").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Normal",
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        public static void SeedOneUser(UserManager<IdentityUser> userManager, string name, string password, string role = null)
        {
            if (userManager.FindByNameAsync(name).Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = name, // musi być taki sam jak email, inaczej nie zadziała
                    Email = name
                };
                IdentityResult result = userManager.CreateAsync(user, password).Result;
                if (result.Succeeded && role != null)
                {
                    userManager.AddToRoleAsync(user, role).Wait();
                }
            }
        }
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            SeedOneUser(userManager, "normaluser@localhost", "nUpass1!", "Normal");
            SeedOneUser(userManager, "adminuser@localhost", "aUpass1!", "Admin");
            SeedOneUser(userManager, "deanuser@localhost", "dUpass1!", "Dean");
        }

    }
}
