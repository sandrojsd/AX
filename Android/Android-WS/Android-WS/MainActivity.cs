using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Android_WS
{
    [Activity(Label = "Android_WS", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            WS.Service S = new WS.Service();
            
            String RET = S.TesteDados(3, true);

            WS.Informacao INF = new WS.Informacao();
            INF.Ativo = true;
            INF.AtivoSpecified = true;
            INF.Nome = "Xamarin";

            S.TesteInformacaoCompleted += S_TesteInformacaoCompleted;
            S.TesteInformacaoAsync(INF);

        }

        void S_TesteInformacaoCompleted(object sender, WS.TesteInformacaoCompletedEventArgs e)
        {
            String Teste = "";
        }
    }
}

