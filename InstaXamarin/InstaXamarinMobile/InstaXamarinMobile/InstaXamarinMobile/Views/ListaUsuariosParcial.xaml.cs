using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaXamarinMobile.Models;
using Xamarin.Forms;
using InstaXamarinMobile.ViewModels;

namespace InstaXamarinMobile.Views
{
    public partial class ListaUsuariosParcial : StackLayout
    {
        ListaUsuariosParcialViewModel LUPVM;

        public ListaUsuariosParcial()
        {
            InitializeComponent();

            LUPVM = new ListaUsuariosParcialViewModel();
            BindingContext = LUPVM;

            ListaUsuarios.ItemTapped += ListaUsuarios_ItemTapped;
        }

        private void ListaUsuarios_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            MessagingCenter.Send<Object, Usuario>(this, "ListaUsuarioItemSelecionado", (Usuario)ListaUsuarios.SelectedItem);
            ListaUsuarios.SelectedItem = null;
        }



        public async Task BuscaUsuarios(String termo)
        {
            await LUPVM.BuscaUsuarios(termo);
        }

        public async Task BuscaSeguidores(int Seguidor)
        {
            await LUPVM.BuscaSeguidores(Seguidor);
        }

        public async Task BuscaSeguidos(int Seguido)
        {
            await LUPVM.BuscaSeguidos(Seguido);
        }

    }
}
