using InstaXamarinMobile.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XLabs.Platform.Services.Media;

using Xamarin.Forms;
using InstaXamarinMobile.Models;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace InstaXamarinMobile.Views
{
    public partial class Perfil : ContentPage
    {
        int IdUsuario;
        PerfilViewModel PVM;

        public Perfil()
        {
            this.IdUsuario = App.UsuarioLogado.Id;
            Inicializa();
        }
        public Perfil(int IdUsuario)
        {
            this.IdUsuario = IdUsuario;
            Title = "Perfil";
            Inicializa();
        }

        public void Inicializa()
        {
            InitializeComponent();

            PVM = new PerfilViewModel(IdUsuario);
            BindingContext = PVM;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Object>(this, "DadosUsuarioAtualizados", (sender) =>
            {
                LOAD.FINALIZA();
                PVM.BuscaPostsUsuario();
            });

            //Recebe dados dos posts do usuário
            MessagingCenter.Subscribe<Object>(this, "PostsUsuarioAtualizados", (sender) =>
            {
                //Limpa as colunas
                PostsColuna1.Children.Clear();
                PostsColuna2.Children.Clear();

                bool PrimeiraColuna = true;

                foreach (var C in PVM.POSTS)
                {
                    PostMiniParcial PMP = new PostMiniParcial(C);
                    PMP.OnClicaPost += PMP_OnClicaPost;

                    if (PrimeiraColuna)
                        PostsColuna1.Children.Add(PMP);
                    else
                        PostsColuna2.Children.Add(PMP);

                    PrimeiraColuna = !PrimeiraColuna;
                }
            });

            if (!AtualizandoFoto)
            {
                LOAD.INICIA("Buscando dados do Usuário...");
                PVM.BuscaDadosUsuario();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Object, Usuario>(this, "DadosUsuarioAtualizados");
            MessagingCenter.Unsubscribe<Object>(this, "PostsUsuarioAtualizados");
        }



        //Seleciona Post
        void PMP_OnClicaPost(Models.Post post)
        {
            Navigation.PushAsync(new PostDetalhado(post));
        }


        private void AlteraDados_Clicked(object sender, EventArgs e)
        {
            AlterarDados AD = new AlterarDados(PVM.USUARIO);
            AD.OnUsuarioAlterado += AD_OnUsuarioAlterado;
            Navigation.PushPopupAsync(AD);
        }

        async void AD_OnUsuarioAlterado(Models.Usuario usarioAlterado)
        {
            LOAD.INICIA("Atualizando dados...");
            await PVM.AlterarDados(usarioAlterado);

            //Atualiza dados locais
            Usuario DadosAtuais = PVM.USUARIO;

            DadosAtuais.Nome = usarioAlterado.Nome;
            DadosAtuais.Email = usarioAlterado.Email;

            PVM.USUARIO = DadosAtuais;

            LOAD.FINALIZA();
        }

        private void Logoff_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<object>(this, "Logoff");
        }


        private async void Seguir_Clicked(object sender, EventArgs e)
        {
            await PVM.SeguirUsuario();
        }

        private async void DeixarSeguir_Clicked(object sender, EventArgs e)
        {
            await PVM.DeixarSeguirUsuario();
        }

        private async void CancelarConta_Clicked(object sender, EventArgs e)
        {
            var confirma = await DisplayAlert("Excluir a conta", "Quer cancelar sua conta e excluir todo o seu conteúdo?", "Sim", "Não");

            if (confirma)
            {
                await PVM.CancelarConta();
                MessagingCenter.Send<object>(this, "Logoff");
            }
        }



        bool AtualizandoFoto;

        async void ClickFoto(object sender, System.EventArgs e)
        {
            if (PVM.USUARIO.NaoMeu)
                return;

            var FonteFoto = await DisplayActionSheet("De onde gostaria de pegar a foto?", "Cancelar", null, "Camêra", "Biblioteca");

            switch (FonteFoto)
            {
                case "Camêra":
                    AtualizandoFoto = true;
                    await PVM.TirarFoto();
                    await PVM.TrocarFoto();
                    AtualizandoFoto = false;
                    break;
                case "Biblioteca":
                    AtualizandoFoto = true;
                    await PVM.BuscarFoto();
                    await PVM.TrocarFoto();
                    AtualizandoFoto = false;
                    break;
                default:
                    break;
            }
        }

        private void ListaSeguidos(object sender, EventArgs e)
        {
            ListaUsuarios LU = new ListaUsuarios();
            LU.Config(ListaUsuarios.TipoLista.Seguidos, PVM.IdUsuario);

            Navigation.PushAsync(LU);
        }

        private void ListaSeguidores(object sender, EventArgs e)
        {
            ListaUsuarios LU = new ListaUsuarios();
            LU.Config(ListaUsuarios.TipoLista.Seguidores, PVM.IdUsuario);

            Navigation.PushAsync(LU);
        }
    }
}