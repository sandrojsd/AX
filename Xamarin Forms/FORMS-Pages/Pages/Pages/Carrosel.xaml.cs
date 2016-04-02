using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Pages
{
    public partial class Carrosel : CarouselPage
    {
        public Carrosel()
        {
            InitializeComponent();
        }

        void CLICK(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TABs());
        }
    }
}
