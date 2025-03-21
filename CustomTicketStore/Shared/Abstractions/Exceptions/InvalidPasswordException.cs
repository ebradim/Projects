namespace CustomTicketStore.Shared.Abstractions.Exceptions;

using Microsoft.AspNetCore.Http;
using CustomTicketStore.Shared.Abstractions.Constants;

public sealed class InvalidPasswordException() : BaseException(ExceptionTokens.PasswordIsInvalid, "Invalid Password.", StatusCodes.Status401Unauthorized);
