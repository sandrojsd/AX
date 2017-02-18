using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using XLabs.Platform.Device;
using XLabs.Ioc;
using InstaXamarinMobile.Models;

namespace InstaXamarinMobile
{
    public partial class App : Application
    {
        #region Preferences Data

        public const string URL = "http://10.10.10.10:4000/";

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

            //MessagingCenter.Subscribe<object>(this, "Logoff", async (sender) => {
            //    using (APIHelper API = new APIHelper())
            //    {
            //        try
            //        {
            //            await API.POST("api/usuario/logoff", new { });
            //        }
            //        catch (Exception)
            //        {
            //        }
            //        UsuarioLogado = null; //Limpa dados
            //        API.HeadersAllRequests = new Dictionary<string, string>();
            //    }

            //    MainPage = new Views.Login();
            //});
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }


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
