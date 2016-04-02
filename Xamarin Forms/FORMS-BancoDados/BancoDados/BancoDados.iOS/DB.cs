using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(BancoDados.iOS.DB))]

namespace BancoDados.iOS
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