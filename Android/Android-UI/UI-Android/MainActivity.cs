using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace UI_Android
{
    [Activity(Label = "UI_Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            Button BTN = FindViewById<Button>(Resource.Id.Botao);

            BTN.Click += BTN_Click;

        }

        void BTN_Click(object sender, EventArgs e)
        {
            EditText TXT = FindViewById<EditText>(Resource.Id.Campo);
            Toast.MakeText(this, TXT.Text, ToastLength.Short).Show();
        }
    }
}

