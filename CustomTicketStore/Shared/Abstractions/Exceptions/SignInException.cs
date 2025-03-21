using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CustomTicketStore.Shared.Abstractions.Constants;

namespace CustomTicketStore.Shared.Abstractions.Exceptions;

public sealed class SignInException(string message) : BaseException(ExceptionTokens.SignInProblem, message, StatusCodes.Status401Unauthorized);
