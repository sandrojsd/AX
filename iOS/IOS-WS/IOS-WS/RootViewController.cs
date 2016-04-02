using System;
using System.Drawing;

using Foundation;
using UIKit;

namespace IOS_WS
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

            WS.Service S = new WS.Service();
            
            String RET = S.TesteDados(1, true);

            WS.Informacao INF = new WS.Informacao();
            INF.Ativo = true;
            INF.Nome = "Xamarin";

            S.TesteInformacaoCompleted += S_TesteInformacaoCompleted;
            S.TesteInformacaoAsync(INF);

        }

        void S_TesteInformacaoCompleted(object sender, WS.TesteInformacaoCompletedEventArgs e)
        {
            
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