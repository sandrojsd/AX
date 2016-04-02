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
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(Files.Droid.File))]

namespace Files.Droid
{
    public class File : IFile
    {
        public string Caminho
        {
            get { return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); }
        }

        public string Ler(string filename)
        {
            return System.IO.File.ReadAllText(Path.Combine(Caminho, filename));
        }

        public Task Salvar(string filename, string text)
        {
            System.IO.File.WriteAllText(Path.Combine(Caminho, filename), text);
            return Task.Delay(0);
        }
    }
}