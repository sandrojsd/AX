using PushNotification.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PushNotification.Plugin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace InstaXamarinMobile.Helpers
{
    //Class to handle push notifications listens to events such as registration, unregistration, message arrival and errors.
    public class  CrossPushNotificationListener : IPushNotificationListener
    {
        public void OnMessage(JObject values, DeviceType deviceType)
        {
            MessagingCenter.Send<Object, int>(this, "NotificacaoRecebida", (int)values["IDPost"]);
        }

        public void OnRegistered(string token, DeviceType deviceType)
        {
            MessagingCenter.Send<Object, String>(this, "PushTokenAtualizado", token);
        }

        public void OnUnregistered(DeviceType deviceType)
        {
            Debug.WriteLine("Push Notification - Device Unnregistered");
        }

        public void OnError(string message, DeviceType deviceType)
        {
            Debug.WriteLine(string.Format("Push notification error - {0}", message));
        }

        public bool ShouldShowNotification()
        {
            return (App.Current == null || App.Current.ATIVO == false);
        }
    }
}
