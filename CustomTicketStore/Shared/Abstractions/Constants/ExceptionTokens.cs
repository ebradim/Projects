namespace CustomTicketStore.Shared.Abstractions.Constants;

internal static class ExceptionTokens
{

    internal readonly static string NoAuthenticationCookie = "USER_CLEARED";
    internal readonly static string AccountNotConfirmed = "ACCOUNT_NOT_CONFIRMED";
    internal readonly static string SignInProblem = "SIGN_IN";
    internal readonly static string EntityWasNotFoundInOurRecord = "NOT_FOUND";
    internal readonly static string AccountIsLocked = "LOCKED";
    internal readonly static string PasswordIsInvalid = "PASSWORD_WRONG";

    internal readonly static string Validation = "VALIDATION";

    internal readonly static string TwoFactorRequiredAuthentication = "2FA_REQUIRED";

}
