using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using System.Collections.Generic;
using System.Linq;

namespace Android_Localizacao
{
    [Activity(Label = "Android_Localizacao", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, ILocationListener
    {
        TextView TXT;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            TXT = FindViewById<TextView>(Resource.Id.Localizacao);


            Inicializa();
        }



        LocationManager LManager;
        String LProvider;

        void Inicializa()
        {
            LManager = (LocationManager)GetSystemService(LocationService);

            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };

            IList<string> acceptableLocationProviders = LManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
                LProvider = acceptableLocationProviders.First();
            else
                LProvider = String.Empty;
        }

        protected override void OnResume()
        {
            base.OnResume();
            LManager.RequestLocationUpdates(LProvider, 0, 0, this);
        }
        protected override void OnPause()
        {
            base.OnPause();
            LManager.RemoveUpdates(this);
        }


        public void OnLocationChanged(Location location)
        {
            if(location != null)
            {
                TXT.Text = "Localização: ";

                TXT.Text += location.Latitude.ToString();
                TXT.Text += " - ";
                TXT.Text += location.Longitude.ToString();
            }
        }

        public void OnProviderDisabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            //throw new NotImplementedException();
        }
    }
}

