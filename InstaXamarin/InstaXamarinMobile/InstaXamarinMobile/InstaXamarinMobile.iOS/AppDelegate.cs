using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Geolocation;

namespace InstaXamarinMobile.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            var resolverContainer = new SimpleContainer();

            resolverContainer
               .Register<IDevice>(t => AppleDevice.CurrentDevice)
               .Register<IGeolocator>(t => new Geolocator());

            Resolver.SetResolver(resolverContainer.GetResolver());

            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule());

            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsMaps.Init();

            FormsPlugin.Iconize.iOS.IconControls.Init();

            LoadApplication(new App());

            //LoadApplication(UXDivers.Gorilla.iOS.Player.CreateApplication(
            //    new UXDivers.Gorilla.Config("Gorilla on WelitonDesktop")));

            return base.FinishedLaunching(app, options);
        }
    }
}
