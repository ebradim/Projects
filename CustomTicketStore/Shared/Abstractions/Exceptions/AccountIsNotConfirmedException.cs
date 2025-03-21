using CustomTicketStore.Shared.Abstractions.Exceptions;

namespace CustomTicketStore.Shared.Abstractions.Exceptions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using CustomTicketStore.Shared.Abstractions.Constants;

public sealed class AccountIsNotConfirmedException() : BaseException(ExceptionTokens.AccountNotConfirmed, $"Your account is not confirmed", StatusCodes.Status403Forbidden);
