using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace GPS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CoreDispatcher DISP;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            DISP = Window.Current.CoreWindow.Dispatcher;
        }

        Geolocator GEO;

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GEO = new Geolocator();
            GEO.MovementThreshold = 5;
            GEO.PositionChanged += GEO_PositionChanged;
        }

        async void GEO_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            await DISP.RunAsync(CoreDispatcherPriority.Normal, () => {
                Geoposition POS = args.Position;
                lblLatitude.Text = POS.Coordinate.Latitude.ToString();
                lblLongitude.Text = POS.Coordinate.Longitude.ToString();
            });
        }
    }
}
