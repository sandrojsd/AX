using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Xamarin.Forms;

namespace WS
{
    public class App : Application
    {
        public App()
        {
            countrySoapClient CS = new countrySoapClient(new BasicHttpBinding(),
                new EndpointAddress("http://www.webservicex.net/country.asmx"));

            CS.GetCountriesCompleted += CS_GetCountriesCompleted;
            CS.GetCountriesAsync();

            // The root page of your application
            //MainPage = new ContentPage
            //{
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            new Label {
            //                XAlign = TextAlignment.Center,
            //                Text = "Welcome to Xamarin Forms!"
            //            }
            //        }
            //    }
            //};
        }

        void CS_GetCountriesCompleted(object sender, GetCountriesCompletedEventArgs e)
        {
            
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
    }
}
