using CustomTicketStore.Shared.Abstractions;
using CustomTicketStore.Shared.Abstractions.CQRS;
using CustomTicketStore.Shared.Abstractions.CQRS.CommandHandling;

namespace CustomTicketStore.Shared.Modules.Accounts.Commands.ClearingSession;


public sealed record class ClearSessionCommand : ICommand<Result<CommandResponse<string>>>
{


    public static Result<ClearSessionCommand> Create()
    {

        return Result<ClearSessionCommand>.Ok(new());
    }

}
