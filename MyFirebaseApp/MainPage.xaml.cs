using MyFirebaseApp.Helpers;

namespace MyFirebaseApp;

public partial class MainPage : ContentPage
{
    FirebaseAuthHelper helper = new FirebaseAuthHelper();

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        var response = await helper.Create("M", "deneme@gmail.com", "deneme123");

        await App.Current.MainPage.DisplayAlert("Firebase Info", response?.GetStatusMessage(), "OK");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var response = await helper.Login("deneme@gmail.com", "deneme1234");

        await App.Current.MainPage.DisplayAlert("Firebase Info", response?.GetStatusMessage(), "OK");
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var response = await helper.ChangePassword("deneme@gmail.com", "deneme1234", "den");

        await App.Current.MainPage.DisplayAlert("Firebase Info", response?.GetStatusMessage(), "OK");
    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        var response = await helper.ChangeDisplayName("deneme@gmail.com", "deneme1234", "MAUI APP");

        await App.Current.MainPage.DisplayAlert("Firebase Info", response?.GetStatusMessage(), "OK");
    }
}
