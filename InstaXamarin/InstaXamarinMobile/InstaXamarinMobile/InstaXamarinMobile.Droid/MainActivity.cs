using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Geolocation;
using ImageCircle.Forms.Plugin.Droid;
using Newtonsoft.Json.Linq;
using PushNotification.Plugin;
using PushNotification.Plugin.Abstractions;

namespace InstaXamarinMobile.Droid
{
    [Activity(Label = "InstaXamarin", Icon = "@drawable/Icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            var resolverContainer = new SimpleContainer();

            resolverContainer
                .Register<IDevice>(t => AndroidDevice.CurrentDevice)
                .Register<IGeolocator>(t => new Geolocator());

            Resolver.ResetResolver();
            Resolver.SetResolver(resolverContainer.GetResolver());

            base.OnCreate(bundle);

            Xamarin.Forms.DependencyService.Register<Custom.AjusteImagem>();
            Xamarin.Forms.DependencyService.Register<Custom.Arquivo>();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            ImageCircleRenderer.Init();

            FormsPlugin.Iconize.Droid.IconControls.Init(Resource.Id.toolbar, Resource.Id.sliding_tabs);

            LoadApplication(new App());

            //De notificação
            if (Intent.Extras != null)
            {
                JObject values = new JObject();
                foreach (var key in Intent.Extras.KeySet())
                {
                    try
                    {
                        //É JSON
                        values.Add(key, JObject.Parse(Intent.Extras.GetString(key)));
                    }
                    catch
                    {
                        values.Add(key, Intent.Extras.GetString(key));
                    }
                }

                CrossPushNotification.PushNotificationListener.OnMessage(values, DeviceType.Android);
            }


            //LoadApplication(UXDivers.Gorilla.Droid.Player.CreateApplication(
            //   ApplicationContext,
            //   new UXDivers.Gorilla.Config("Gorilla on WelitonDesktop")));

        }
    }
}

