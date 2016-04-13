using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BlogCCA;
using BlogCCA.WinPhone;
using System.IO;
using SQLite.Net.Interop;
using Windows.Storage;
using System.Threading.Tasks;
using Microsoft.Phone.Controls;
using System.Windows;

[assembly: Xamarin.Forms.Dependency(typeof(RecursosNativos))]

namespace BlogCCA.WinPhone
{
    public class RecursosNativos : iRecursosNativos
    {
        public ISQLitePlatform SQLitePlataform
        {
            get
            {
                return new SQLite.Net.Platform.WindowsPhone8.SQLitePlatformWP8();
            }
        }

        public string GetCaminho(String Arquivo)
        {
            var documentsPath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            return Path.Combine(documentsPath, Arquivo);
        }

    }
}