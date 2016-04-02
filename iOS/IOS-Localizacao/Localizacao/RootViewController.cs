using System;
using System.Drawing;

using Foundation;
using UIKit;

using CoreLocation;

namespace Localizacao
{
    public partial class RootViewController : UIViewController
    {
        public RootViewController(IntPtr handle)
            : base(handle)
        {
        }

        CLLocationManager GPS;

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


            GPS = new CLLocationManager();

            GPS.DesiredAccuracy = CLLocation.AccuracyBest;

            GPS.LocationsUpdated += GPS_LocationsUpdated;

            GPS.RequestWhenInUseAuthorization();

            GPS.StartUpdatingLocation();

        }

        void GPS_LocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
        {
            if (e.Locations.Length > 0)
            {
                lblLatitude.Text = e.Locations[0].Coordinate.Latitude.ToString("0.00000000");
                lblLongitude.Text = e.Locations[0].Coordinate.Longitude.ToString("0.00000000");

                //GPS.StopUpdatingLocation();
            }
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