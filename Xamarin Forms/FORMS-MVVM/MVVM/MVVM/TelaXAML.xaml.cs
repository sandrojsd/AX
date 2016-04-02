using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MVVM
{
    public partial class TelaXAML : ContentPage
    {
        EstruturaTeste TESTE;

        public TelaXAML()
        {
            TESTE = new EstruturaTeste();

            TESTE.Nome = "Nome111";

            BindingContext = TESTE;

            InitializeComponent();
            
        }

        void OnAlteraNome(object sender, EventArgs e)
        {
            TESTE.Nome = Guid.NewGuid().ToString();
        }
    }
}
