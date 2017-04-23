using InstaXamarinMobile.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace InstaXamarinMobile.Views
{
    public partial class ListaUsuarios : ContentPage
    {
        public enum TipoLista
        {
            Seguidos,
            Seguidores
        }

        TipoLista Tipo;
        int IdUsuario;

        public ListaUsuarios()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Object, Usuario>(this, "ListaUsuarioItemSelecionado", (sender, usuario) => {
                Navigation.PushAsync(new Perfil(usuario.Id));
            });
        }

        public async void Config(TipoLista tipo, int idUsuario)
        {
            Tipo = tipo;
            IdUsuario = idUsuario;

            switch (tipo)
            {
                case TipoLista.Seguidos:
                    Title = "Quem ele segue";
                    LOAD.INICIA("Trazendo quem o usuário segue...");
                    await Lista.BuscaSeguidos(IdUsuario);
                    LOAD.FINALIZA();
                    break;
                case TipoLista.Seguidores:
                    Title = "Quem segue ele";
                    LOAD.INICIA("Trazendo quem segue o usuário...");
                    await Lista.BuscaSeguidores(IdUsuario);
                    LOAD.FINALIZA();
                    break;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Object, Usuario>(this, "ListaUsuarioItemSelecionado");
        }

    }
}
