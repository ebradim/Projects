namespace CustomTicketStore.Shared.Abstractions;

using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using CustomTicketStore.Shared.Abstractions.Exceptions;

public class Result<T>
{

    private Result(T value)
    {
        Value = value;
    }
    private Result(BaseException error)
    {
        Error = error;
    }
    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool Succeeded => Error is null && Value is not null;
    public T? Value { get; private set; }
    public BaseException? Error { get; private set; }

    public static Result<T> Ok(T value) => new(value);
    public static Result<T> Fail(BaseException error) => new(error);
}
