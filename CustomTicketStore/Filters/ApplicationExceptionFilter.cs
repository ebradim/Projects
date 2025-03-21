using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using CustomTicketStore.Shared.Abstractions.Exceptions;

namespace CustomTicketStore.Clients.CMSApp.Filters;


public sealed class ApplicationExceptionFilter(ILogger<ApplicationExceptionFilter> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An exception was thrown");

        var baseException = exception as BaseException;
        var contextStatusCode = baseException is not null ? baseException.CodeIdentifier : StatusCodes.Status500InternalServerError;
        httpContext.Response.StatusCode = contextStatusCode;
        var result = new ProblemDetails()
        {
            Title = baseException is not null ? baseException.Token : "An error occurred while processing your request",
            Detail = exception.Message,
            Status = contextStatusCode,
            Type = exception.GetType().Name,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
            Extensions =
            {
                {"traceId",httpContext.TraceIdentifier }
            }
        };
        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
        return true;
    }
}