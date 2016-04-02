
using System;
using System.Drawing;

using Foundation;
using UIKit;

namespace ListasIOS
{
    public partial class Celula : UITableViewCell
    {
        public Celula(IntPtr handle)
            : base(handle)
        {
        }

        public void MontaCelula(String Dado)
        {
            lblItem.Text = Dado;
            imgItem.Image = UIImage.FromFile("IMG.png");
        }

    }
}