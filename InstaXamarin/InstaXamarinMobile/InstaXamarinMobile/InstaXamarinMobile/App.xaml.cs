using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using XLabs.Platform.Device;
using XLabs.Ioc;
using InstaXamarinMobile.Models;
using XLabs.Platform.Services.Geolocation;

namespace InstaXamarinMobile
{
    public partial class App : Application
    {
        #region Preferences Data

        //public const string URL = "http://10.10.10.10:4000/";
        public const string URL = "http://instaxamarin.azurewebsites.net/";

        public static App Current;

        public string PushToken
        {
            get { return App.PreferenceGet<String>("PushToken"); }
            set { App.PreferenceAdd("PushToken", value); }
        }
        public static Usuario UsuarioLogado
        {
            get { return App.PreferenceGet<Usuario>("UsuarioLogado"); }
            set { App.PreferenceAdd("UsuarioLogado", value); }
        }
        public static bool Logado
        {
            get { return UsuarioLogado != null; }
        }

        public static int LarguraTela
        {
            get { return App.PreferenceGet<int>("LarguraTela"); }
            set { App.PreferenceAdd("LarguraTela", value); }
        }
        public static int AlturaTela
        {
            get { return App.PreferenceGet<int>("AlturaTela"); }
            set { App.PreferenceAdd("AlturaTela", value); }
        }

        #endregion

        public App()
        {
            InitializeComponent();

            Current = this;


            LarguraTela = Resolver.Resolve<IDevice>().Display.Width;
            AlturaTela = Resolver.Resolve<IDevice>().Display.Height;

            if (Logado)
                MainPage = new NavigationPage(new Views.Home());
            else
                MainPage = new Views.Login();

            MessagingCenter.Subscribe<object>(this, "Logado", (sender) =>
            {
                MainPage = new NavigationPage(new Views.Home());
            });

            MessagingCenter.Subscribe<object>(this, "Logoff", async (sender) =>
            {
                using (APIHelper API = new APIHelper())
                {
                    try
                    {
                        await API.POST("api/usuario/logoff", new { });
                    }
                    catch (Exception)
                    {
                    }
                    UsuarioLogado = null; //Limpa dados
                    API.HeadersAllRequests = new Dictionary<string, string>();
                }

                MainPage = new Views.Login();
            });
        }


        protected override void OnStart()
        {
            // Handle when your app starts
            ColetaLocalizacao();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            ColetaLocalizacao();
        }


        #region Localizacao

        public static double Latitude
        {
            get { return App.PreferenceGet<double>("Latitude"); }
            set { App.PreferenceAdd("Latitude", value); }
        }
        public static double Longitude
        {
            get { return App.PreferenceGet<double>("Longitude"); }
            set { App.PreferenceAdd("Longitude", value); }
        }
        public static bool Localizado
        {
            get { return (Latitude != 0 && Longitude != 0); }
        }

        public static String PositionStatus { get; set; }

        public void ColetaLocalizacao()
        {
            if (geolocator == null)
                geolocator = DependencyService.Get<IGeolocator>() ?? Resolver.Resolve<IGeolocator>();

            this.geolocator.GetPositionAsync(timeout: 10000)
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                        PositionStatus = ((GeolocationException)t.Exception.InnerException).Error.ToString();
                    else if (t.IsCanceled)
                        PositionStatus = "Cancelado";
                    else
                    {
                        PositionStatus = t.Result.Timestamp.ToString("G");
                        Latitude = t.Result.Latitude;
                        Longitude = t.Result.Longitude;

                        MessagingCenter.Send<Object>(this, "LocalizacaoEncontrada");
                    }

                });
        }

        IGeolocator geolocator;

        #endregion


        #region Preferences

        public static void PreferenceAdd(string Name, object Data)
        {
            string Serializado = JsonConvert.SerializeObject(Data);

            Current.Properties[Name] = Serializado;

            Current.SavePropertiesAsync();
        }

        public static bool PreferenceExists(string Name)
        {
            return Current.Properties.ContainsKey(Name);
        }
        public static void PreferenceRemove(string Name)
        {
            Current.Properties.Remove(Name);
            Current.SavePropertiesAsync();
        }

        public static T PreferenceGet<T>(string Name)
        {
            if (Current.Properties.ContainsKey(Name))
            {
                string Dado = Current.Properties[Name].ToString();
                return JsonConvert.DeserializeObject<T>(Dado);
            }

            return default(T);

        }

        #endregion
    }
}
