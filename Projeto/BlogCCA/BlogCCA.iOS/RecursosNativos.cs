using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

using BlogCCA;
using BlogCCA.iOS;
using System.IO;
using SQLite.Net.Interop;

[assembly: Xamarin.Forms.Dependency(typeof(RecursosNativos))]

namespace BlogCCA.iOS
{
    public class RecursosNativos : iRecursosNativos
    {
        public ISQLitePlatform SQLitePlataform
        {
            get
            {
                return new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
            }
        }

        public string GetCaminho(String Arquivo)
        {
            var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
            return Path.Combine (documentsPath, Arquivo);
        }


    }
}