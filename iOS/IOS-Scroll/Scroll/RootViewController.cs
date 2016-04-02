using System;
using System.Drawing;

using Foundation;
using UIKit;

namespace Scroll
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

            // Perform any additional setup after loading the view, typically from a nib.

            UIView Conteudo = new UIView(new CoreGraphics.CGRect(10, 10, 280, 480));
            Conteudo.BackgroundColor = UIColor.Yellow;

            scroll.AddSubview(Conteudo);
            scroll.ContentSize = new CoreGraphics.CGSize(300, 500);


        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            scroll.Frame = new CoreGraphics.CGRect(scroll.Frame.X, scroll.Frame.Y,
                300, 100);
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