using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CustomTicketStore.Shared.Infrastructures.Persistence;

public static class DataSeed
{
    public static async Task CreateTestAsync(this WebApplication web, CancellationToken cancellationToken)
    {
        await using var scope = web.Services.CreateAsyncScope();
        var serviceProvider = scope.ServiceProvider;
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser<int>>>();
        if (!await userManager.Users.AnyAsync(cancellationToken))
        {
            var user = new IdentityUser<int>()
            {
                Email = "test@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+20123",
                PhoneNumberConfirmed = true,
                UserName = "test01",
            };
            var createResult = await userManager.CreateAsync(user, "Test@123");
            if (!createResult.Succeeded)
                throw new Exception(createResult.ToString());
        }

    }
}
