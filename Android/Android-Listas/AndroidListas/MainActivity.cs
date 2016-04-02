using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace AndroidListas
{
    [Activity(Label = "AndroidListas", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            DADOS = new List<string>();

            DADOS.Add("Item 1");
            DADOS.Add("Item 2");
            DADOS.Add("Item 3");
            DADOS.Add("Item 4");

            ListView List = FindViewById<ListView>(Resource.Id.ListaDados);

            GerenciamentoLista GL = new GerenciamentoLista(DADOS, this);

            List.Adapter = GL;
            List.ItemClick += List_ItemClick;

        }

        List<String> DADOS;

        void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, DADOS[e.Position], ToastLength.Short).Show();
        }
    }
}

