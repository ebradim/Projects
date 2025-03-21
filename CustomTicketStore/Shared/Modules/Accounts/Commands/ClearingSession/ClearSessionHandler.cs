namespace CustomTicketStore.Modules.Accounts.Commands.ClearingSession;

using Microsoft.AspNetCore.Identity;
using CustomTicketStore.Shared.Abstractions;
using CustomTicketStore.Shared.Abstractions.CQRS;
using CustomTicketStore.Shared.Abstractions.CQRS.CommandHandling;
using System.Threading;
using System.Threading.Tasks;
using CustomTicketStore.Shared.Modules.Accounts.Commands.ClearingSession;

internal sealed class ClearSessionHandler(SignInManager<IdentityUser<int>> signInManager) : ICommandHandler<ClearSessionCommand, Result<CommandResponse<string>>>
{

    public async Task<Result<CommandResponse<string>>> Handle(ClearSessionCommand request, CancellationToken cancellationToken)
    {

        await signInManager.SignOutAsync();

        return Result<CommandResponse<string>>.Ok(new("Succeeded", nameof(ClearSessionHandler)));
    }
}
