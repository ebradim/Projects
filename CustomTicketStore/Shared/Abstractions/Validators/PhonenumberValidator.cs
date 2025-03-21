namespace CustomTicketStore.Shared.Abstractions.Validators;
using System.Text.RegularExpressions;

public static partial class PhonenumberValidator
{
    public static bool IsValid(string phone)
    {
        return PhonenumberRegex().IsMatch(phone);
    }

    [GeneratedRegex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex PhonenumberRegex();

}
