using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Android1
{
    [Activity(Label = "Tela 1", MainLauncher = true, Icon = "@drawable/icon")]
    public class Tela1 : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Tela1);

            FindViewById<TextView>(Resource.Id.textView1).Click += MainActivity_Click;    
        }

        void MainActivity_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Tela2));
        }

    }
}

