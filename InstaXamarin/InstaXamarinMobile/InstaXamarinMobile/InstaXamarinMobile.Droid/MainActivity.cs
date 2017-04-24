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

namespace InstaXamarinMobile.Droid
{
    [Activity(Label = "InstaXamarinMobile", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
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

            Resolver.SetResolver(resolverContainer.GetResolver());

            base.OnCreate(bundle);

            Xamarin.Forms.DependencyService.Register<Custom.AjusteImagem>();
            Xamarin.Forms.DependencyService.Register<Custom.Arquivo>();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            ImageCircleRenderer.Init();

            FormsPlugin.Iconize.Droid.IconControls.Init(Resource.Id.toolbar, Resource.Id.sliding_tabs);

            LoadApplication(new App());

            //LoadApplication(UXDivers.Gorilla.Droid.Player.CreateApplication(
            //   ApplicationContext,
            //   new UXDivers.Gorilla.Config("Gorilla on WelitonDesktop")));

        }
    }
}

