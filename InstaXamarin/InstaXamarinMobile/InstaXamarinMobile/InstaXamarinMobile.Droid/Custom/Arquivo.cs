using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using InstaXamarinMobile.Custom;
using Android.Gms.Common.Images;
using Android.Media;
using System.IO;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(IArquivo))]

namespace InstaXamarinMobile.Droid.Custom
{
    public class Arquivo : IArquivo
    {
        public string LocalBanco(string NomeArquivo)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, NomeArquivo);
        }
    }
}