using System.ComponentModel.DataAnnotations;
using CustomTicketStore.Shared.Abstractions.Validators;

namespace CustomTicketStore.Controllers.API.Accounts.Requests;

public sealed record class LoginRequest([Required][RegularExpression(UserNameValidator.Regex)] string UserName, [Required] string Password, bool Persisted);
