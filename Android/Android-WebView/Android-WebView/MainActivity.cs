using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;
using System.Text;

namespace Android_WebView
{
    [Activity(Label = "Android_WebView", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            WebView WV = FindViewById<WebView>(Resource.Id.webv);

            //WV.LoadUrl("http://comocriaraplicativos.com.br");

            StringBuilder STR = new StringBuilder();
            STR.Append("<html>");
            STR.Append("<body>");
            STR.Append("<img src='Teste.png' />");
            STR.Append("</body>");
            STR.Append("</html>");

            WV.LoadDataWithBaseURL("file:///android_asset/",
                STR.ToString(), "text/html", "utf-8", null);

        }
    }
}

