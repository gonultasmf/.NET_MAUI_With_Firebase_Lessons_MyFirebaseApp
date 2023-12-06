using MyFirebaseApp.Helpers;

namespace MyFirebaseApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        FirebaseAuthHelper helper = new FirebaseAuthHelper();

        var auth = await helper.Create("MAUI User Name", "deneme@gmail.com", "d");

        await App.Current.MainPage.DisplayAlert("Firebase Info", auth?.GetStatusMessage(), "OK");
    }
}
