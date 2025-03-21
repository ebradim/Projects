namespace CustomTicketStore.Shared.Modules.Accounts.Commands.EstablishingSession.LocalSignIn;

using Microsoft.AspNetCore.Identity;
using NodaTime;
using CustomTicketStore.Shared.Abstractions;
using CustomTicketStore.Shared.Abstractions.CQRS;
using CustomTicketStore.Shared.Abstractions.CQRS.CommandHandling;
using CustomTicketStore.Shared.Abstractions.Exceptions;
using CustomTicketStore.Shared.Abstractions.UserAccessor;
using CustomTicketStore.Shared.Abstractions.Validators;
using CustomTicketStore.Shared.Infrastructures.WebAPI.UserAccessor;
using System.Threading;
using System.Threading.Tasks;


internal sealed class SignInHandler(SignInManager<IdentityUser<int>> signInManager, IUserAccessor userAccessor) : ICommandHandler<SignInCommand, Result<CommandResponse<string>>>
{
    public async Task<Result<CommandResponse<string>>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {


        IdentityUser<int>? user;
        if (EmailValidator.IsValid(request.Credential))
            user = await signInManager.UserManager.FindByEmailAsync(request.Credential);
        else
            user = await signInManager.UserManager.FindByNameAsync(request.Credential);
        if (user is null)
            return Result<CommandResponse<string>>.Fail(new RecordNotFoundException($"'{request.Credential}' was not found"));


        var signInResult = await signInManager.PasswordSignInAsync(user, request.Password, request.Persisted, true);

        if (!signInResult.Succeeded)
        {
            if (signInResult.IsNotAllowed)
                return Result<CommandResponse<string>>.Fail(new AccountIsNotConfirmedException());

            if (signInResult.IsLockedOut)
                return Result<CommandResponse<string>>.Fail(new LockedOutException());

            if (signInResult.RequiresTwoFactor)
                return Result<CommandResponse<string>>.Fail(new TwoFactorRequiredException());

            return Result<CommandResponse<string>>.Fail(new InvalidPasswordException());

        }

        //User is already signed in
        return Result<CommandResponse<string>>.Ok(new(userAccessor.GetName()!, nameof(SignInHandler)));

    }
}
