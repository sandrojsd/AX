using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Geolocation;
using ImageCircle.Forms.Plugin.iOS;
using InstaXamarinMobile.Helpers;
using PushNotification.Plugin;

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

            Xamarin.Forms.DependencyService.Register<Custom.AjusteImagem>();
            Xamarin.Forms.DependencyService.Register<Custom.Arquivo>();

            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule());

            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsMaps.Init();
            ImageCircleRenderer.Init();

            FormsPlugin.Iconize.iOS.IconControls.Init();

            LoadApplication(new App());

            CrossPushNotification.Initialize<CrossPushNotificationListener>();

            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;

            //LoadApplication(UXDivers.Gorilla.iOS.Player.CreateApplication(
            //    new UXDivers.Gorilla.Config("Gorilla on WelitonDesktop")));

            return base.FinishedLaunching(app, options);
        }


        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            if (CrossPushNotification.Current is IPushNotificationHandler)
            {
                ((IPushNotificationHandler)CrossPushNotification.Current).OnErrorReceived(error);
            }
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            if (CrossPushNotification.Current is IPushNotificationHandler)
            {
                ((IPushNotificationHandler)CrossPushNotification.Current).OnRegisteredSuccess(deviceToken);
            }
        }

        public override void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
        {
            application.RegisterForRemoteNotifications();
        }

        /* Uncomment if using remote background notifications. To support this background mode, enable the Remote notifications option from the Background modes section of iOS project properties. (You can also enable this support by including the UIBackgroundModes key with the remote-notification value in your app�s Info.plist file.)
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
			if (CrossPushNotification.Current is IPushNotificationHandler) 
			{
				((IPushNotificationHandler)CrossPushNotification.Current).OnMessageReceived(userInfo);
			}
        }
        */

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            if (CrossPushNotification.Current is IPushNotificationHandler)
            {
                ((IPushNotificationHandler)CrossPushNotification.Current).OnMessageReceived(userInfo);
                UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
            }
        }
    }
}
