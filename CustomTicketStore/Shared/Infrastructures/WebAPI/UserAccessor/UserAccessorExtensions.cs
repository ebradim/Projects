namespace CustomTicketStore.Shared.Infrastructures.WebAPI.UserAccessor;

using Microsoft.Extensions.DependencyInjection;
using CustomTicketStore.Shared.Abstractions.Exceptions;
using CustomTicketStore.Shared.Abstractions.UserAccessor;
using System.Security.Claims;
using static CustomTicketStore.Shared.Abstractions.Constants.ApplicationDefaults;

public static class UserAccessorExtensions
{
    public static void AddUserAccessor(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUserAccessor, BaseUserAccessor>();

    }

    /// <summary>
    /// Reads the <see cref="ClaimTypes.NameIdentifier"/> for the current user.
    /// </summary>
    /// <param name="userAccessor"></param>
    /// <returns>User Id</returns>
    public static int? GetUserId(this IUserAccessor userAccessor)
    {
        if (!userAccessor.IsAuthenticated)
            return default;

        if (!int.TryParse(userAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out int id))
            return null;
        return id;
    }
    /// <summary>
    /// Reads the <see cref="ClaimTypes.Name"/> for the current user.
    /// <para> This method uses the default implementation of <c>HttpContext.User.Identity?.Name</c> for reading that value </para>
    /// </summary>
    /// <param name="userAccessor"></param>
    /// <returns>The Username</returns>
    public static string? GetName(this IUserAccessor userAccessor)
    {
        if (!userAccessor.IsAuthenticated)
            return default;
        return userAccessor.HttpContext.User.Identity?.Name;
    }
    /// <summary>
    /// Reads the <see cref="ClaimTypes.Email"/> for the current user
    /// </summary>
    /// <param name="userAccessor"></param>
    /// <returns>The email</returns>
    /// <exception cref="UserAccessorException"></exception>
    public static string? GetEmail(this IUserAccessor userAccessor)
    {
        if (!userAccessor.IsAuthenticated)
            return default;
        return userAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
    }
    /// <summary>
    /// Reads the <see cref="ClaimTypes.Role"/> for the current user
    /// </summary>
    /// <param name="userAccessor"></param>
    /// <returns>List of roles</returns>
    /// <exception cref="UserAccessorException"></exception>
    private static IEnumerable<string> GetRoles(this IUserAccessor userAccessor)
    {
        if (!userAccessor.IsAuthenticated)
            return [];
        var roles = userAccessor.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
        return roles is null ? [] : roles;
    }

    public static bool IsInAnyRoles(this IUserAccessor userAccessor, params string[] roles)
    {
        if (!userAccessor.IsAuthenticated)
            return false;

        return GetRoles(userAccessor).Any(e => roles.Contains(e, StringComparer.OrdinalIgnoreCase));
    }

}
