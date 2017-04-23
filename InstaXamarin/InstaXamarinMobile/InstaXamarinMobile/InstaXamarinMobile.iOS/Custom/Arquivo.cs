using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using InstaXamarinMobile.Custom;
using Xamarin.Forms;
using System.Drawing;
using CoreGraphics;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(IArquivo))]

namespace InstaXamarinMobile.iOS.Custom
{
    public class Arquivo : IArquivo
    {
        public string LocalBanco(string NomeArquivo)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, NomeArquivo);
        }
    }
}