using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

using Xamarin.Forms;

namespace Files
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
							XAlign = TextAlignment.Center,
							Text = "Welcome to Xamarin Forms!"
						}
					}
                }
            };

            //CarregaArquivo();
            ManipulaArquivos();
        }

        void CarregaArquivo()
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Files.Config.txt");
            string ConteudoTexto = "";
            using (var reader = new System.IO.StreamReader(stream))
                ConteudoTexto = reader.ReadToEnd();
        }

        async void ManipulaArquivos()
        {
            String CaminhoBase = DependencyService.Get<IFile>().Caminho;

            await DependencyService.Get<IFile>().Salvar("ARQUIVO.TXT", "DADO DO ARQUIVO");

            String Recuperacao = DependencyService.Get<IFile>().Ler("ARQUIVO.TXT");
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
