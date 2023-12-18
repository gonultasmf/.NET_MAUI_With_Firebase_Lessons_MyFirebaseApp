using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Extensions.Logging;
using MyFirebaseApp.Models;
using Newtonsoft.Json;

namespace MyFirebaseApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            //RegistrarFirebase();

            //QueryFirebase();

            //QueryFirebase_1();

            //QueryFirebase_2();

            return builder.Build();
        }


        public static async void RegistrarFirebase()
        {
            FirebaseClient client = new FirebaseClient("https://fir-maui-9f935-default-rtdb.europe-west1.firebasedatabase.app/");
            var animalsModel = client.Child(nameof(Animals));
            var parentModel = client.Child(nameof(ParentModel));
            var animals = await animalsModel.OnceAsync<Animals>();
            var parents = await parentModel.OnceAsync<ParentModel>();

            if (animals.Count <= 0)
            {
                await animalsModel.PostAsync(JsonConvert.SerializeObject(new Animals
                {
                    Name = "Kedi",
                    Color = "Beyaz"
                }));
                await animalsModel.PostAsync(JsonConvert.SerializeObject(new Animals
                {
                    Name = "Köpek",
                    Color = "Siyah"
                }));
                await animalsModel.PostAsync(JsonConvert.SerializeObject(new Animals
                {
                    Name = "Fare",
                    Color = "Turuncu"
                }));
            }

            if (parents.Count <= 0)
            {
                await parentModel.PostAsync(JsonConvert.SerializeObject(new ParentModel
                {
                    AyakSayisi = 4,
                    Animal = animals.Select(x => x.Object).FirstOrDefault(x => x.Name == "Kedi")
                }));
                await parentModel.PostAsync(JsonConvert.SerializeObject(new ParentModel
                {
                    AyakSayisi = 4,
                    Animal = animals.Select(x => x.Object).FirstOrDefault(x => x.Name == "Köpek")
                }));
                await parentModel.PostAsync(JsonConvert.SerializeObject(new ParentModel
                {
                    AyakSayisi = 4,
                    Animal = animals.Select(x => x.Object).FirstOrDefault(x => x.Name == "Fare")
                }));
            }

        }

        public static async void QueryFirebase()
        {
            FirebaseClient client = new FirebaseClient("https://fir-maui-9f935-default-rtdb.europe-west1.firebasedatabase.app/");
            var animalsModel = client.Child(nameof(Animals));
            var animals = await animalsModel.OnceAsync<Animals>();
            var temp = await animalsModel.OnceSingleAsync<Animals>();
            var animalList = animals.Select(x => x.Object).ToList();
        }

        public static async void QueryFirebase_1()
        {
            FirebaseClient client = new FirebaseClient("https://fir-maui-9f935-default-rtdb.europe-west1.firebasedatabase.app/");
            var parentModel = client.Child(nameof(ParentModel));
            

            await parentModel.PostAsync(JsonConvert.SerializeObject(new ParentModel
            {
                AyakSayisi = 2,
                Animal = new Animals
                {
                    Name = "Tavuk",
                    Color = "Kahverengi"
                }
            }));
            var parents = await parentModel.OnceAsync<ParentModel>();
            var animalList = parents.Select(x => x.Object).ToList();
        }

        public static async void QueryFirebase_2()
        {
            FirebaseClient client = new FirebaseClient("https://fir-maui-9f935-default-rtdb.europe-west1.firebasedatabase.app/");
            var model = client.Child(nameof(ParentModel)).Child("-Nl4cCNn9a-9aN9Rejhg");
            var parentModel = await model.OnceSingleAsync<ParentModel>();
            //parentModel.AyakSayisi = 10;
            //await client.Child(nameof(ParentModel)).PostAsync(new ParentModel
            //{
            //    AyakSayisi = 2,
            //    Animal = new Animals
            //    {
            //        Color = "Kırmızı",
            //        Name = "Horoz"
            //    }
            //});

            //await model.PutAsync(parentModel);
            await model.DeleteAsync();
        }
    }
}
