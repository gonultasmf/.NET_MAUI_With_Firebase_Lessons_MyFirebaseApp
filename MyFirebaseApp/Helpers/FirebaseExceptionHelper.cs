namespace MyFirebaseApp.Helpers;

public static class FirebaseExceptionHelper
{
    public static string GetStatusMessage(this string message)
    {
        if (message == null)
            return string.Empty;

        var resultMessage = string.Empty;

        if (message.Contains("EMAIL_EXISTS"))
            resultMessage += "Bu mail adresi zaten kayıtlı!\n";

        if (message.Contains("INVALID_EMAIL"))
            resultMessage += "Girilen email adresi hatalı!\n";

        if (message.Contains("WEAK_PASSWORD"))
            resultMessage += "Şifre en az 6 karakterli olmalı!\n";

        if (message.Contains("INVALID_LOGIN_CREDENTIALS"))
            resultMessage += "Girilen email adresi veya şifre yanlış!\n";


        return !string.IsNullOrEmpty(resultMessage) ? resultMessage : message;
    }
}
