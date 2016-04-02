using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace NavegacaoIOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window
        {
            get;
            set;
        }
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            return true;
        }
    }
}