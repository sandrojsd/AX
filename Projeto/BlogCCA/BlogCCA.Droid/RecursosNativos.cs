using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BlogCCA;
using BlogCCA.Android;
using System.IO;
using SQLite.Net.Interop;

[assembly: Xamarin.Forms.Dependency(typeof(RecursosNativos))]

namespace BlogCCA.Android
{
    public class RecursosNativos : iRecursosNativos
    {
        public ISQLitePlatform SQLitePlataform
        {
            get
            {
                return new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            }
        }

        public string GetCaminho(String Arquivo)
        {
            var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
            return Path.Combine (documentsPath, Arquivo);
        }


    }
}