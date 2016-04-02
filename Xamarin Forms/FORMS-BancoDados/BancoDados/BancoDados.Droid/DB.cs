using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BancoDados;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(BancoDados.Droid.DB))]

namespace BancoDados.Droid
{
    public class DB : ISQLite
    {
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "TESTE.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }
    }
}