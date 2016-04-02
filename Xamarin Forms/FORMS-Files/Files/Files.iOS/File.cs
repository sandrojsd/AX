using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using System.IO;
using System.Threading.Tasks;

[assembly: Dependency(typeof(Files.iOS.File))]

namespace Files.iOS
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