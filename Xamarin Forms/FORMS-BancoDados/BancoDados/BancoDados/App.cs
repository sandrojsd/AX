using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BancoDados
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            Text = "Banco de dados!"
                        }
                    }
                }
            };

            DB BANCO = new DB();

            BANCO.AdicionaTesteDado();
            BANCO.AdicionaTesteDado();
            BANCO.AdicionaTesteDado();

            var resultado = BANCO.GetTesteDados();
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
