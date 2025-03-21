namespace CustomTicketStore.Shared.Abstractions.Exceptions;

using Microsoft.AspNetCore.Http;
using CustomTicketStore.Shared.Abstractions.Constants;

public sealed class LockedOutException() : BaseException(ExceptionTokens.AccountIsLocked, "Account is locked out.", StatusCodes.Status423Locked);