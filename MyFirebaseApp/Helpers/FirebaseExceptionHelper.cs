namespace MyFirebaseApp.Helpers;

public static class FirebaseExceptionHelper
{
    public static string GetStatusMessage(this string message)
    {
        if (message == null)
            return string.Empty;

        var resultMessage = string.Empty;

        if (message.Contains("EMAIL_EXISTS"))
            resultMessage += "Bu mail adresi zaten kayıtlı!";

        if (message.Contains("INVALID_EMAIL"))
            resultMessage += "Girilen email adresi hatalı!";


        return !string.IsNullOrEmpty(resultMessage) ? resultMessage : message;
    }
}
