using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

using Xamarin.Forms;
using RestSharp.Portable;
using BlogCCA.Telas;
using System.Threading.Tasks;
using FrazzApps.Xamarin.GoogleAnalyticsConnector;

namespace BlogCCA
{
    public class App : Application
    {
        public static NavigationPage NAV;
        GoogleAnalytics GA;

        public App()
        {
            MainPage = new Home();
        }

        protected override void OnStart()
        {
            RegistraAcesso();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            RegistraAcesso();
        }


        void RegistraAcesso()
        {
            GA = new GoogleAnalytics(
               "UA-17713535-12",
               "AppCCA",
               "",
               "",
               "");
            GA.TrackPage("APP", Device.OnPlatform("IOS", "ANDROID", "WPHONE") + " - ACESSO");
        }
    }


   



}
