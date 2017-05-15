using InstaXamarinMobile.Models;
using InstaXamarinMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace InstaXamarinMobile.Views
{
    public partial class Feed : ContentPage
    {
        FeedViewModel FVM;

        public Feed()
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.iOS)
                Title = "Feed";

            FVM = new FeedViewModel();
            BindingContext = FVM;

            Posts.Refreshing += Posts_Refreshing;
            Posts.IsPullToRefreshEnabled = true;

            Posts.ItemAppearing += Posts_ItemAppearing;

            AtualizaPosts();

            ToolbarItems.Add(new ToolbarItem("Atualizar", "", async () =>
            {
                await AtualizaPosts();
            }));

            Posts.ItemSelected += Posts_ItemSelected;

            MessagingCenter.Subscribe<Object, Post>(this, "PostExcluido", (Sender, PostRemovido) => {
                PostSelecionado = null;
            });
        }

        public async Task AtualizaPosts()
        {
            //LOAD.INICIA("Buscando Fotos...");

            FVM.Pagina = 1;
            FVM.SemMaisDados = false;

            Posts.IsRefreshing = true;

            await FVM.BuscaPostsCache();
            await FVM.BuscaPosts(); //API

            Posts.IsRefreshing = false;

            //LOAD.FINALIZA();
        }

        private async void Posts_Refreshing(object sender, EventArgs e)
        {
            Posts.IsRefreshing = true;
            FVM.Pagina = 1;
            FVM.SemMaisDados = false;
            await FVM.BuscaPosts();
            Posts.IsRefreshing = false;
        }

        private async void Posts_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (((Post)e.Item) == FVM.POSTS.Last() && !Posts.IsRefreshing)
            {
                if (!FVM.SemMaisDados)
                {
                    //Add Buscando mais
                    FVM.Pagina++;
                    await FVM.BuscaPosts();
                }
            }
        }

        Post PostSelecionado;

        private void Posts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                PostSelecionado = (Post)e.SelectedItem;

                //Remove selecionado
                Posts.SelectedItem = null;

                //Chama tela de detalhe
                Navigation.PushAsync(new PostDetalhado(PostSelecionado));
            }
        }
    }
}
