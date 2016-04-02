using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Pages
{
    public partial class MD : MasterDetailPage
    {
        public MD()
        {
            InitializeComponent();
        }

        void DATA(object sender, EventArgs e)
        {
            txtData.Text = DateTime.Now.ToString();
        }
    }
}
