using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PushSDK;

namespace BlogCCA.WinPhone
{
    public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new BlogCCA.App());

            NotificationService service = NotificationService.GetCurrent("271D1-B0DFB", null, null);
            service.OnPushTokenReceived += (sender, pushToken) =>
            {
                //code to handle push token
            };

            service.OnPushTokenFailed += (sender, error) =>
            {
                //code to handle push subscription failure
            };

            service.OnPushAccepted += (sender, pushNotification) =>
            {
                //code to handle push notification
            };

            service.SubscribeToPushService();

        }
        
    }
}
