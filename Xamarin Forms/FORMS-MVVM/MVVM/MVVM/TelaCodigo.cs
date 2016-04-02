using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace MVVM
{
    public class TelaCodigo : ContentPage
    {
        EstruturaTeste TESTE;

        public TelaCodigo()
        {
            TESTE = new EstruturaTeste();

            TESTE.Nome = "Nome111";

            BindingContext = TESTE;


            Label L = new Label();
            L.SetBinding(Label.TextProperty, (EstruturaTeste T) => T.Nome);

            Label L2 = new Label();
            L2.SetBinding(Label.TextProperty, (EstruturaTeste T) => T.Idade);


            Button B1 = new Button();
            B1.Text = "Aumentar Idade";
            B1.Command = TESTE.AumentarIdade;
            B1.CommandParameter = "2";

            Button B2 = new Button();
            B2.Text = "Reduzir 1 Idade";
            B2.Command = TESTE.ReduzirIdade;

            Button B3 = new Button();
            B3.Text = "Alterar Nome";
            B3.Clicked += B3_Clicked;
    

            Content = new StackLayout
            {
                Children = {
					L,
                    L2,
                    B1,
                    B2,
                    B3
				}
            };
        }

        void B3_Clicked(object sender, EventArgs e)
        {
            TESTE.Nome = Guid.NewGuid().ToString();
        }
    }
}
