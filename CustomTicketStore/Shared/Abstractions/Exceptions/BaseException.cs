namespace CustomTicketStore.Shared.Abstractions.Exceptions;

using System;

public abstract class BaseException(string token, string message, int codeIdentifier) : Exception(message)
{
    public string Token { get; } = token;
    public int CodeIdentifier { get; } = codeIdentifier;

}
