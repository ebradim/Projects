using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomTicketStore.Shared.Abstractions.CQRS.CommandHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace CustomTicketStore.Controllers.Server.Accounts;

public sealed class AccountController() : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (User.Identity?.IsAuthenticated is true)
            context.Result = new RedirectToActionResult("index", "home", default);
    }
    [HttpGet]
    public IActionResult Login()
    {

        return View();
    }

}
