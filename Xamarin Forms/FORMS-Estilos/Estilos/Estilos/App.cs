using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Estilos
{
    public class App : Application
    {
        public App()
        {
            Style TESTE = new Style(typeof(Label))
            {
                Setters = {
                    new Setter {Property = Label.BackgroundColorProperty, Value = Color.Green},
                    new Setter {Property = Label.TextColorProperty, Value = Color.Blue},
                }
            };


            // The root page of your application
            //MainPage = new ContentPage
            //{
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            new Label {
            //                XAlign = TextAlignment.Center,
            //                Text = "Welcome to Xamarin Forms!",
            //                Style = TESTE
            //            }
            //        }
            //    }
            //};

            MainPage = new Page1();
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
