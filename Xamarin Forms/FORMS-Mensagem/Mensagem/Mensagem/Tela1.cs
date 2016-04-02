using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mensagem
{
    public class Tela1 : ContentPage
    {
        public Label LABEL { get; set; }
        Entry ENTRADA;

        public Tela1()
        {
            LABEL = new Label();
            LABEL.Text = "sem dados";

            ENTRADA = new Entry();

            Button BTN = new Button();
            BTN.Clicked +=BTN_Clicked;

            Content = new StackLayout()
            {
                Children = {
                    ENTRADA,
                    BTN,
                    LABEL
                }

            };
            
        }

        void BTN_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<Tela1, String>(this, "EnvioTexto", ENTRADA.Text);
        }

    }
}
