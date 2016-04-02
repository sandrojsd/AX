using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Alertas
{
    public partial class Alertas : ContentPage
    {
        public Alertas()
        {
            InitializeComponent();
        }

        async void CLICK_BTN(object sender, EventArgs e)
        {
            //var ret = await DisplayAlert("Pergunta", "Mensagem", "Sim", "Não");

            //if (ret)
            //{
            //    LBL.Text = "SIM";
            //}
            //else 
            //{
            //    LBL.Text = "NÃO";
            //}

            var ret = await DisplayActionSheet("Pergunta", "Cancelar", "PERIGO", "OP1", "OP2");
            LBL.Text = ret;
        }
    }
}
