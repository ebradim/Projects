using CustomTicketStore.Controllers.API.Accounts.Requests;
using CustomTicketStore.Shared.Abstractions;
using CustomTicketStore.Shared.Abstractions.CQRS.CommandHandling;
using CustomTicketStore.Shared.Modules.Accounts.Commands.ClearingSession;
using CustomTicketStore.Shared.Modules.Accounts.Commands.EstablishingSession.LocalSignIn;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CustomTicketStore.Controllers.API.Accounts;

[ApiController]
[Route("api/[controller]")]
public class AccountsController(ICommandBus commandBus) : ControllerBase
{


    [AllowAnonymous]
    [HttpPost("login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LocalSignInAsync([FromForm] LoginRequest login, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var command = SignInCommand.Create(login.UserName, login.Password, true);
        if (!command.Succeeded)
        {
            return ValidationProblem(command.ToProblemDetails(HttpContext.TraceIdentifier, $"{Request.Method} {Request.Path}"));
        }

        var result = await commandBus.Send(command.Value, cancellationToken);
        if (!result.Succeeded)
            return Problem(result.Error.Message, statusCode: result.Error.CodeIdentifier, title: result.Error.Token, instance: $"{Request.Method} {Request.Path}");

        return Ok(result.Value);

    }

}
