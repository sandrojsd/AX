using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Mensagem
{
    public class App : Application
    {

        Tela1 T1;
        public App()
        {
            // The root page of your application
            T1 = new Tela1();
            MainPage = T1;

            MessagingCenter.Subscribe<Tela1, String>(this, "EnvioTexto", (sender, dado) => {
                Device.BeginInvokeOnMainThread(() => {
                    T1.LABEL.Text = dado;
                });
            });
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
