using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CustomTicketStore.Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CustomTicketStore.Shared.Abstractions;

public static class ResultExtensions
{
    public static ValidationProblemDetails ToProblemDetails<T>(this Result<T> result, string tractId, string instance)
    {
        if (result.Error is not ValidationFailedException validation)
            throw new InvalidOperationException("Result is not a validation failed exception");
        return new ValidationProblemDetails()
        {
            Errors = { new(validation.Parameter, [result.Error?.Message ?? "Internal Validation Error"]) },
            Title = result.Error?.Token,
            Status = result.Error?.CodeIdentifier,
            Detail = result.Error?.Message,
            Instance = instance,
            Extensions =
            {
                {"traceId",tractId }
            }
        };
    }


}