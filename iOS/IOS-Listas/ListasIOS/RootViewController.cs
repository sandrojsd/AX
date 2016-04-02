using System;
using System.Drawing;

using Foundation;
using UIKit;
using System.Collections.Generic;

namespace ListasIOS
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

            List<String> DADOS = new List<string>();
            DADOS.Add("Item 1");
            DADOS.Add("Item 2");
            DADOS.Add("Item 3");
            DADOS.Add("Item 4");

            GerenciamentoDados GD = new GerenciamentoDados(DADOS);

            tabela.Source = GD;
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

    public class GerenciamentoDados : UITableViewSource
    {
        List<String> DADOS;

        public GerenciamentoDados(List<String> dados)
        {
            DADOS = dados;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            Celula CELL = (Celula)tableView.DequeueReusableCell("CELL");

            CELL.MontaCelula(DADOS[indexPath.Row]);

            return CELL;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return nint.Parse(DADOS.Count.ToString());
        }
    }
}