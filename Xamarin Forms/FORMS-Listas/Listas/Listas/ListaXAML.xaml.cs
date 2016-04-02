using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Listas
{
    public partial class ListaXAML : ContentPage
    {
        DB BancoDados;

        public ListaXAML()
        {
            InitializeComponent();

            BancoDados = new DB();
            BancoDados.AdicionaItens();

            LISTA.ItemsSource = BancoDados.DADOS;
            LISTA.ItemAppearing += LISTA_ItemAppearing;
        }

        void LISTA_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var DADOS_LISTA = (IList)LISTA.ItemsSource;
            DB.DADO D_DADO_ULTIMO = (DB.DADO)DADOS_LISTA[DADOS_LISTA.Count - 1];

            if (((DB.DADO)e.Item).ID == D_DADO_ULTIMO.ID)
            {
                BancoDados.AdicionaItens();
            }
        }
    }
}
