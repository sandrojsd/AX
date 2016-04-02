using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Listas
{
    public class App : Application
    {
        DB BancoDados;

        public App()
        {

            //List<String> DADOS = new List<string>();
            //DADOS.Add(Guid.NewGuid().ToString());
            //DADOS.Add(Guid.NewGuid().ToString());
            //DADOS.Add(Guid.NewGuid().ToString());
            //DADOS.Add(Guid.NewGuid().ToString());
            //DADOS.Add(Guid.NewGuid().ToString());

            //ListView LV = new ListView();
            //LV.ItemsSource = DADOS;

            //BancoDados = new DB();
            //BancoDados.AdicionaItens();

            //ListView LV = new ListView();
            //LV.ItemsSource = BancoDados.DADOS;

            //var TEMPLATE = new DataTemplate(typeof(TextCell));
            //TEMPLATE.SetBinding(TextCell.TextProperty, "ID");
            //TEMPLATE.SetBinding(TextCell.DetailProperty, "Nome");

            //LV.ItemTemplate = TEMPLATE;


            //LV.ItemAppearing += (sender, e) => {
            //    var DADOS_LISTA = (IList)LV.ItemsSource;
            //    DB.DADO D_DADO_ULTIMO = (DB.DADO)DADOS_LISTA[DADOS_LISTA.Count-1];

            //    if (((DB.DADO)e.Item).ID == D_DADO_ULTIMO.ID)
            //    {
            //        BancoDados.AdicionaItens();
            //    }
            //};


            //// The root page of your application
            //MainPage = new ContentPage
            //{
            //    Content = new StackLayout
            //    {
            //        Children = {
            //            LV
            //        }
            //    }
            //};

            MainPage = new ListaXAML();
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
