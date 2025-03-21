namespace CustomTicketStore.Shared.Abstractions.Validators;

using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

public static partial class UserNameValidator
{
    public static bool IsValid(string userName)
    {
        return UserNameRegexValidation().IsMatch(userName)
            && userName.Length >= MinLength && userName.Length <= MaxLength;
    }
    public const int MaxLength = 26;
    public const int MinLength = 5;
    [StringSyntax(StringSyntaxAttribute.Regex)]
    public const string Regex = "^[a-zA-Z0-9]+$";
    [GeneratedRegex(Regex, RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex UserNameRegexValidation();
}
