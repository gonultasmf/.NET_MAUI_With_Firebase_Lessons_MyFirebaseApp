using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using MyFirebaseApp.Helpers;
using MyFirebaseApp.Models;

namespace MyFirebaseApp;

public partial class MainPage : ContentPage
{
    FirebaseAuthHelper helper = new FirebaseAuthHelper();

    public MainPage()
    {
        InitializeComponent();
        MyMethod();
    }

    private async void MyMethod()
    {
        FirebaseClient client = new FirebaseClient("https://fir-maui-9f935-default-rtdb.europe-west1.firebasedatabase.app/");
        var model = await client.Child(nameof(Animals)).OnceAsync<Animals>();

        var temp = model.FirstOrDefault(x => !string.IsNullOrEmpty(x.Object.ImageUrl))?.Object;

        if (temp != null)
        {
            logo.Source = temp.ImageUrl;
            txtName.Text = temp.Name;
            txtColor.Text = temp.Color;
        }
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

    private async void Button_Clicked_3(object sender, EventArgs e)
    {
        var foto = await MediaPicker.PickPhotoAsync();
        if (foto != null)
        {
            var stream = await foto.OpenReadAsync();
            var urlFoto = await new FirebaseStorage("fir-maui-9f935.appspot.com")
                                    .Child("Fotolar")
                                    .Child(DateTime.Now.ToString("ddMMyyyyhhmmss") + foto.FileName)
                                    .PutAsync(stream);

            FirebaseClient client = new FirebaseClient("https://fir-maui-9f935-default-rtdb.europe-west1.firebasedatabase.app/");
            var model = client.Child(nameof(Animals));
            await model.PostAsync(new Animals
            {
                Name = "MAUI",
                Color = "Mavi",
                ImageUrl = urlFoto
            });

            logo.Source = urlFoto;
        }
    }
}
