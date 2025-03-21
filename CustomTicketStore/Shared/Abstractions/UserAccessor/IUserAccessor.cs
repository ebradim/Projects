namespace CustomTicketStore.Shared.Abstractions.UserAccessor;

using Microsoft.AspNetCore.Http;
using CustomTicketStore.Shared.Abstractions.Constants;
using CustomTicketStore.Shared.Abstractions.Exceptions;
using static CustomTicketStore.Shared.Abstractions.Constants.ApplicationDefaults;

public interface IUserAccessor
{
    public HttpContext HttpContext { get; }
    // public string? ClientIP { get; }

    /// <summary>
    /// Check if there is a user in the current request by invoking <see cref="System.Security.Principal.IIdentity.IsAuthenticated"/>
    /// </summary>
    public bool IsAuthenticated => HttpContext.User.Identity is not null && HttpContext.User.Identity.IsAuthenticated;

}
