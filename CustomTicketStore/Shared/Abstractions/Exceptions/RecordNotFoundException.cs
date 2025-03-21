namespace CustomTicketStore.Shared.Abstractions.Exceptions;

using Microsoft.AspNetCore.Http;
using CustomTicketStore.Shared.Abstractions.Constants;

public sealed class RecordNotFoundException(string message) : BaseException(ExceptionTokens.EntityWasNotFoundInOurRecord, message, StatusCodes.Status404NotFound);
