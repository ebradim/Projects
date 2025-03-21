namespace CustomTicketStore.Shared.Infrastructures.WebAPI.UserAccessor;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using CustomTicketStore.Shared.Abstractions.Constants;
using CustomTicketStore.Shared.Abstractions.UserAccessor;
using System;
using System.Net;
using static CustomTicketStore.Shared.Abstractions.Constants.ApplicationDefaults;

internal sealed class BaseUserAccessor : IUserAccessor
{
    // private const string EXTERNAL_LOGIN_PATH_1 = "/account/complete";
    // private const string EXTERNAL_LOGIN_PATH_2 = "/account/login";
    // private const string CHANGE_EMAIL_PATH_1 = "/account/change-email";
    // private const string EXTERNAL_GOOGLE_LOGIN = "https://accounts.google.com/";
    // private static readonly List<string> _knownWebClients =
    // [
    //     EXTERNAL_LOGIN_PATH_1,
    //     EXTERNAL_LOGIN_PATH_2,
    //     CHANGE_EMAIL_PATH_1,
    //     EXTERNAL_GOOGLE_LOGIN
    // ];
    public BaseUserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        //ArgumentNullException.ThrowIfNull(httpContextAccessor.HttpContext, nameof(httpContextAccessor));

        HttpContext = httpContextAccessor.HttpContext;

    }
    public HttpContext HttpContext { get; init; }



    // public string? ClientIP
    // {
    //     get
    //     {
    //         var headers = HttpContext.Request.Headers;
    //         var forwardedFor = headers["X-Forwarded-For"].ToString();
    //         var remoteIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

    //         var clientId = !string.IsNullOrEmpty(forwardedFor) ? forwardedFor : remoteIpAddress;

    //         if (string.IsNullOrEmpty(clientId))
    //         {
    //             return default;
    //         }

    //         // Handle multiple IP addresses in X-Forwarded-For header
    //         if (clientId.Contains(','))
    //         {
    //             clientId = clientId.Split(',')[0].Trim();
    //         }

    //         // Remove port information if present
    //         var ipAddressWithoutPort = clientId.Split(':')[0];

    //         if (IPAddress.TryParse(ipAddressWithoutPort, out var ip))
    //         {
    //             // Return the IP address if it's valid and not a loopback address
    //             if (!IPAddress.IsLoopback(ip))
    //             {
    //                 return ipAddressWithoutPort;
    //             }
    //             return clientId; // return the loopback address
    //         }

    //         return default;
    //     }
    // }



    public bool IsAuthenticated => HttpContext?.User?.Identity?.IsAuthenticated is true;




}
