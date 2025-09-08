using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using MyData;
using MyShop.Entities.Model;

namespace LapShoo.Services
{
    public class SeedService
    {
        public static async Task SeedDatabase (IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope ();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext> ();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Useres>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();
            try
            {
                // sure data base is here
                logger.LogInformation("be sure date base is crated");
                await context.Database.EnsureCreatedAsync ();

                // set roles 
                logger.LogInformation("Seeding roles");
                await AddRoleAsync(roleManager, "Admin");
                await AddRoleAsync(roleManager, "User");
                await AddRoleAsync(roleManager, "Emp");

                // Add admin user
                logger.LogInformation("Seeding admin user");
                var adminEmail = "admin@gmail.com";
                if(await userManager.FindByEmailAsync(adminEmail) == null) 
                {
                    var adminUser = new Useres
                    {
                        FirstName = "Sohayb",
                        UserName = "Sam",
                        NormalizedUserName = "Sam".ToUpper(),
                        LastName = "Zayan",
                        Email = adminEmail,
                        NormalizedEmail = adminEmail.ToUpper(),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Country = "Egypt"
                    };

                    var result = await userManager.CreateAsync(adminUser, "Admin@123");
                    if (result.Succeeded)
                    {
                        logger.LogInformation("✅ Admin user created successfully.");
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                    else
                    {
                        logger.LogError("❌ Failed to create admin user. Errors:");
                        foreach (var error in result.Errors)
                        {
                            logger.LogError($"   - Code: {error.Code}, Description: {error.Description}");
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");

            }




        }

        private static async Task AddRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }


    }
}
