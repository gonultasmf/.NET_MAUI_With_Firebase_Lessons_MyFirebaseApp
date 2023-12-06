using Firebase.Auth;
using Firebase.Auth.Providers;

namespace MyFirebaseApp.Helpers;

public class FirebaseAuthHelper
{
    private static FirebaseAuthConfig _config = new FirebaseAuthConfig()
    {
        ApiKey = "AIzaSyDVAVJwUzg1XjjitRQAT5DrAO8kbX0LC_Y",
        AuthDomain = "fir-maui-9f935.firebaseapp.com",
        Providers = new FirebaseAuthProvider[]
        {
            new EmailProvider()
        }
    };

    FirebaseAuthClient client = new FirebaseAuthClient(_config);

    public async Task<string?>? Create(string username, string email, string password)
    {
        try
        {
            var response = await client.CreateUserWithEmailAndPasswordAsync(email, password, username);

            return response?.User?.Uid?.ToString();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        
    }
}
