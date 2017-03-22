using CoreGraphics;
using CoreLocation;
using Foundation;
using InstaXamarinMobile.Custom;
using InstaXamarinMobile.Models;
using MapKit;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using UIKit;

namespace InstaXamarinMobile.iOS.Custom
{
    public class MapAnnotation : MKAnnotation
    {
        CLLocationCoordinate2D coord;
        public override CLLocationCoordinate2D Coordinate { get { return coord; } }
        public override void SetCoordinate(CLLocationCoordinate2D value)
        { coord = value; }

        PinoFoto _PinoFoto;
        public PinoFoto PinoFoto { get { return _PinoFoto; } }

        public override string Title { get { return "-"; } }

        public MapAnnotation(CLLocationCoordinate2D coordinate, PinoFoto pinoFoto)
        {
            this.coord = coordinate;
            this._PinoFoto = pinoFoto;
        }
    }

    public class CustomMKAnnotationView : MKAnnotationView
    {
        public Post POST { get; set; }

        public NSData ImagemCache { get; set; }

        public CustomMKAnnotationView(IMKAnnotation annotation, string id)
            : base(annotation, id)
        { }

        public override UIView HitTest(CGPoint point, UIEvent uievent)
        {
            UIView hitView = base.HitTest(point, uievent);
            if (hitView != null)
                base.Superview.BringSubviewToFront(this);

            return hitView;
        }

        public override bool PointInside(CGPoint point, UIEvent uievent)
        {
            CGRect rect = this.Bounds;
            var isInside = rect.Contains(point);

            if (!isInside)
            {
                foreach (UIView view in this.Subviews)
                {
                    isInside = view.Frame.Contains(point);
                    if (isInside)
                        break;
                }
            }
            return isInside;
        }
    }

}
