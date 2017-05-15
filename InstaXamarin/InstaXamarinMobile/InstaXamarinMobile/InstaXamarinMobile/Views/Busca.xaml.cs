using InstaXamarinMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace InstaXamarinMobile.Views
{
    public partial class Busca : ContentPage
    {
        public Busca()
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.iOS)
                Title = "Busca";

            txtBusca.SearchButtonPressed += Busca_SearchButtonPressed;
            txtBusca.HeightRequest = 40;
        }

        private async void Busca_SearchButtonPressed(object sender, EventArgs e)
        {
            LOAD.INICIA("Realizando busca de usuários...");
            await ListaUsuarios.BuscaUsuarios(txtBusca.Text);
            LOAD.FINALIZA();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Object, Usuario>(this, "ListaUsuarioItemSelecionado", (sender, usuario) => {
                Navigation.PushAsync(new Perfil(usuario.Id));
            });
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Object, Usuario>(this, "ListaUsuarioItemSelecionado");
        }
    }
}
