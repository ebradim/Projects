namespace CustomTicketStore.Shared.Abstractions.Exceptions;

using Microsoft.AspNetCore.Http;
using CustomTicketStore.Shared.Abstractions.Constants;

public sealed class UserAccessorException(string message) : BaseException(ExceptionTokens.NoAuthenticationCookie, message, StatusCodes.Status401Unauthorized);
