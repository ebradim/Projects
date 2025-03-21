using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CustomTicketStore.Shared.Abstractions.Constants;

namespace CustomTicketStore.Shared.Abstractions.Exceptions;

public sealed class ValidationFailedException(string parameter, string error) : BaseException(ExceptionTokens.Validation, error, StatusCodes.Status400BadRequest)
{
    public string Parameter { get; init; } = parameter;
}
