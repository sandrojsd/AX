using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;

namespace Android_Arquivos
{
    [Activity(Label = "Android_Arquivos", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            String FILE = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Arquivo.TXT");

            StreamWriter SW = new StreamWriter(FILE);
            SW.WriteLine("TESTE");
            SW.Flush();
            SW.Close();

            StreamReader SR = new StreamReader(FILE);
            string TXT = SR.ReadLine();
            SR.Close();

        }
    }
}

