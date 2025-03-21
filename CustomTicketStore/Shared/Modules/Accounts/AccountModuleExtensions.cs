using CustomTicketStore.Shared.Infrastructures.Persistence;
using Microsoft.AspNetCore.Identity;

namespace CustomTicketStore.Shared.Modules.Accounts;

public static class AccountModuleExtensions
{
    public static void AddAccountsModule(this IServiceCollection services)
    {
        services
            .AddIdentityCore<IdentityUser<int>>(e =>
            {
                e.Password.RequiredLength = 6;
                e.Password.RequireNonAlphanumeric = false;
                e.Password.RequireUppercase = false;
                e.Password.RequireLowercase = false;
                e.Password.RequireDigit = false;
                e.User.RequireUniqueEmail = true;
                e.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_.";
                e.SignIn.RequireConfirmedEmail = true;
            })
            .AddSignInManager<SignInManager<IdentityUser<int>>>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<DataContext>();
    }
}
