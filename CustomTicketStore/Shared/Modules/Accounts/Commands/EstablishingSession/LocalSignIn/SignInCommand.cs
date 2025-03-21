using Microsoft.AspNetCore.Identity;
using CustomTicketStore.Shared.Abstractions;
using CustomTicketStore.Shared.Abstractions.CQRS;
using CustomTicketStore.Shared.Abstractions.CQRS.CommandHandling;
using CustomTicketStore.Shared.Abstractions.Exceptions;
using CustomTicketStore.Shared.Abstractions.Validators;
using StackExchange.Redis;

namespace CustomTicketStore.Shared.Modules.Accounts.Commands.EstablishingSession.LocalSignIn;


public sealed record class SignInCommand : ICommand<Result<CommandResponse<string>>>
{
    private SignInCommand(string credential, string password, bool persisted)
    {
        Credential = credential;
        Password = password;
        Persisted = persisted;
    }


    public string Credential { get; }
    public string Password { get; }
    public bool Persisted { get; }

    public static Result<SignInCommand> Create(string credential, string password, bool persisted)
    {
        if (!EmailValidator.IsValid(credential) && !UserNameValidator.IsValid(credential))
            return Result<SignInCommand>.Fail(new ValidationFailedException(nameof(Credential), "Credential is not valid"));

        if (string.IsNullOrEmpty(password))
            return Result<SignInCommand>.Fail(new ValidationFailedException(nameof(Password), "Password is required"));


        return Result<SignInCommand>.Ok(new SignInCommand(credential, password, persisted));
    }
}
