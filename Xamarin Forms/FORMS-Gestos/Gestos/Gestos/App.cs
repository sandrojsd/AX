using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Gestos
{
    public class App : Application
    {
        public App()
        {
            //var TAP = new TapGestureRecognizer();
            //TAP.Tapped += TAP_Tapped;


            //Label LABEL = new Label();
            //LABEL.XAlign = TextAlignment.Center;
            //LABEL.Text = "Welcome to Xamarin Forms!";

            //LABEL.GestureRecognizers.Add(TAP);

            //// The root page of your application
            //MainPage = new ContentPage
            //{
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            LABEL
            //        }
            //    }
            //};

            MainPage = new Page1();
        }

        void TAP_Tapped(object sender, EventArgs e)
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
