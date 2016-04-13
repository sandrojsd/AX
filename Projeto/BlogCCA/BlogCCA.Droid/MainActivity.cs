using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ArelloMobile.Push;
using ArelloMobile.Push.Utils;

using Android.Content;
using Android.Support.V4.App;
using Android.Util;
using Org.Json;

namespace BlogCCA.Droid
{
    class LocalMessageBroadcastReceiver : BasePushMessageReceiver
    {
        public MainActivity activity { get; set; }

        protected override void OnMessageReceive(Intent intent)
        {
            activity.doOnMessageReceive(intent.GetStringExtra(BasePushMessageReceiver.JsonDataKey));
        }
    }

    class LocalRegisterBroadcastReceiver : RegisterBroadcastReceiver
    {
        public MainActivity activity { get; set; }

        protected override void OnRegisterActionReceive(Context p0, Intent intent)
        {
            activity.checkMessage(intent);
        }
    }

    [IntentFilter(new string[] { "br.com.comocriaraplicativos.app.MESSAGE" }, Categories = new string[] { "android.intent.category.DEFAULT" })]
    [Activity(Label = "BlogCCA", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Theme = "@style/CustomTheme")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        LocalMessageBroadcastReceiver mMessageReceiver;
        LocalRegisterBroadcastReceiver mRegisterReceiver;

        bool mBroadcastPush = true;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            mMessageReceiver = new LocalMessageBroadcastReceiver();
            mMessageReceiver.activity = this;

            mRegisterReceiver = new LocalRegisterBroadcastReceiver();
            mRegisterReceiver.activity = this;
            registerReceivers();


            try
            {
                ArelloMobile.Push.PushManager manager = ArelloMobile.Push.PushManager.GetInstance(this);
                manager.OnStartup(this);
                //Register for push!
                manager.RegisterForPushNotifications();

                checkMessage(Intent);
            }
            catch 
            {}
            

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

        }

        protected override void OnNewIntent(Intent intent)
        {
            checkMessage(intent);
        }

        public void checkMessage(Intent intent)
        {
            if (null != intent)
            {
                if (intent.HasExtra(PushManager.PushReceiveEvent))
                {
                    doOnMessageReceive(intent.Extras.GetString(PushManager.PushReceiveEvent));
                }
                else if (intent.HasExtra(PushManager.RegisterEvent))
                {
                    doOnRegistered(intent.Extras.GetString(PushManager.RegisterEvent));
                }
                else if (intent.HasExtra(PushManager.UnregisterEvent))
                {
                    doOnUnregisteredError(intent.Extras.GetString(PushManager.UnregisterEvent));
                }
                else if (intent.HasExtra(PushManager.RegisterErrorEvent))
                {
                    doOnRegisteredError(intent.Extras.GetString(PushManager.RegisterErrorEvent));
                }
                else if (intent.HasExtra(PushManager.UnregisterErrorEvent))
                {
                    doOnUnregistered(intent.Extras.GetString(PushManager.UnregisterErrorEvent));
                }

                resetIntentValues();
            }
        }

        public void doOnRegistered(String registrationId)
        {
            // code to run if device has succesfully registered
        }

        public void doOnRegisteredError(String errorId)
        {
            // code to run if device failed to register
        }

        public void doOnUnregistered(String registrationId)
        {
            // code to run if device has succesfully unregistered
        }

        public void doOnUnregisteredError(String errorId)
        {
            // code to run if device failed to unregister properly
        }

        public void doOnMessageReceive(String message)
        {
            // code to run when device receives notification
        }

        /**
     * Will check main Activity intent and if it contains any Pushwoosh data,
     * will clear it
*/
        private void resetIntentValues()
        {
            Intent mainAppIntent = Intent;

            if (mainAppIntent.HasExtra(PushManager.PushReceiveEvent))
            {
                mainAppIntent.RemoveExtra(PushManager.PushReceiveEvent);
            }
            else if (mainAppIntent.HasExtra(PushManager.RegisterEvent))
            {
                mainAppIntent.RemoveExtra(PushManager.RegisterEvent);
            }
            else if (mainAppIntent.HasExtra(PushManager.UnregisterEvent))
            {
                mainAppIntent.RemoveExtra(PushManager.UnregisterEvent);
            }
            else if (mainAppIntent.HasExtra(PushManager.RegisterErrorEvent))
            {
                mainAppIntent.RemoveExtra(PushManager.RegisterErrorEvent);
            }
            else if (mainAppIntent.HasExtra(PushManager.UnregisterErrorEvent))
            {
                mainAppIntent.RemoveExtra(PushManager.UnregisterErrorEvent);
            }

            Intent = mainAppIntent;
        }

        protected override void OnResume()
        {
            base.OnResume();

            registerReceivers();
        }

        protected override void OnPause()
        {
            base.OnPause();

            unregisterReceivers();
        }

        public void registerReceivers()
        {
            IntentFilter intentFilter = new IntentFilter(PackageName + ".action.PUSH_MESSAGE_RECEIVE");

            if (mBroadcastPush)
            {
                RegisterReceiver(mMessageReceiver, intentFilter);
            }

            RegisterReceiver(mRegisterReceiver, new IntentFilter(PackageName + "." + PushManager.RegisterBroadCastAction));
        }

        public void unregisterReceivers()
        {
            UnregisterReceiver(mMessageReceiver);
            UnregisterReceiver(mRegisterReceiver);
        }
    }


    
}

