using System;
using System.Drawing;

using Foundation;
using UIKit;
using CoreLocation;
using MapKit;

namespace IOS_Mapas
{
    public partial class RootViewController : UIViewController
    {
        public RootViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            CLLocationManager locationManager = new CLLocationManager();
            if (locationManager.RespondsToSelector(new ObjCRuntime.Selector("requestWhenInUseAuthorization")))
            {
                locationManager.RequestWhenInUseAuthorization();
            }

            MKPointAnnotation pinView = new MKPointAnnotation();
            pinView.Title = "Teste";
            pinView.SetCoordinate(new CoreLocation.CLLocationCoordinate2D(-23.548428, -46.631834));
            mapa.AddAnnotation(pinView);

            mapa.Camera = new MKMapCamera()
            {
                CenterCoordinate = new CoreLocation.CLLocationCoordinate2D(-23.548428, -46.631834),
                Altitude = 3000
            };

            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        #endregion
    }
}