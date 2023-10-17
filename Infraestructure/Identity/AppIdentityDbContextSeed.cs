using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infraestructure.Identity;

public class AppIdentityDbContextSeed
{
    public static async Task SeedUserAsync(UserManager<AppUser> userManager)
    {
        if(!userManager.Users.Any())
        {
            var user = new AppUser
            {
                DisplayName = "Admin",
                Email = "admin@test.com",
                UserName = "admin@test.com",
                Address = new Address
                {
                    FirstName = "Admin",
                    LastName = "Bruno",
                    Street = "10 the Street",
                    City = "New York",
                    State = "NY",
                    ZipCode = "90210"
                }
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }
}
