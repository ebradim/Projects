namespace CustomTicketStore.Shared.Abstractions.Exceptions;

using CustomTicketStore.Shared.Abstractions.Constants;
using Microsoft.AspNetCore.Http;

public sealed class TwoFactorRequiredException(int statusCode = StatusCodes.Status401Unauthorized) : BaseException(ExceptionTokens.TwoFactorRequiredAuthentication, "2FA is enabled", statusCode);
