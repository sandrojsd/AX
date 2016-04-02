using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using SQLite;
using System.Collections.Generic;
using System.IO;

namespace Android_BancoDados
{
    [Activity(Label = "Android_BancoDados", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var db = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "DB"));

            db.CreateTable<Cliente>();

            db.Insert(new Cliente() { Nome = "Cliente1" });
            db.Insert(new Cliente() { Nome = "Cliente2" });

            List<Cliente> CLIS = db.Table<Cliente>().ToList();

            db.Close();
            
        }
    }
}

