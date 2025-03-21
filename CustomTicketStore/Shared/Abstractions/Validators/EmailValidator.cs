namespace CustomTicketStore.Shared.Abstractions.Validators;

using System.Text.RegularExpressions;

public static partial class EmailValidator
{
    public static bool IsValid(string email)
    {
        return EmailRegex().IsMatch(email);
    }

    [GeneratedRegex("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex EmailRegex();
}
