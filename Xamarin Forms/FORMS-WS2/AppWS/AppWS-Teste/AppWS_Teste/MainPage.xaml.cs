using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppWS_Teste
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            DependencyService.Get<iWS>().Gravar(new EstruturaDados() { ID=12, Nome="www", Data=DateTime.Now });
            List<EstruturaDados> Dados = DependencyService.Get<iWS>().Listar();
        }
    }
}
