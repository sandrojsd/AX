using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Imagens
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
						new Image {
                            Source = Device.OnPlatform(
                                iOS: ImageSource.FromFile("Imagens/CCA.png"),
                                Android: ImageSource.FromFile("CCA.png"),
                                WinPhone: ImageSource.FromFile("CCA.png")
                            )
                        }
					}
                }
            };

            //Source = ImageSource.FromFile("CCA.png")
            //Source = ImageSource.FromUri(new Uri("http://comocriaraplicativos.com.br/wp-content/uploads/2015/02/Logo150x70@2x.png"))
            //Source = ImageSource.FromResource("Imagens.CCA.png")
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
