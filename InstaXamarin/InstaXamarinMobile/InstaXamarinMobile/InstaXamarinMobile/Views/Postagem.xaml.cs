using InstaXamarinMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace InstaXamarinMobile.Views
{
    public partial class Postagem : ContentPage
    {
        PostagemViewModel PostagemVM;

        public Postagem()
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.iOS)
                Title = "Postar";

            PostagemVM = new PostagemViewModel();
            BindingContext = PostagemVM;

            btnPostar.Clicked += BtnPostar_Clicked;
        }

        private async void BtnPostar_Clicked(object sender, EventArgs e)
        {
            //Valida Foto
            if (PostagemVM.POST.Foto == null)
            {
                await DisplayAlert("Erro", "Precisa de uma foto para postar", "OK");
                return;
            }

            //Valida Descrição
            if (String.IsNullOrEmpty(PostagemVM.POST.Descricao))
            {
                await DisplayAlert("Erro", "Informe uma descrição", "OK");
                return;
            }

            LOAD.INICIA("Gravando Postagem...");

            //Posta na API
            await PostagemVM.Postar();

            //Finaliza e mostra mensagem
            LOAD.FINALIZA();

            txtDescricao.Text = "";

            await DisplayAlert("Postado!", "Postagem realizada com sucesso.", "OK");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void AbreTeclado(object sender, EventArgs e)
        {
            if (Device.OS == TargetPlatform.Android)
                EspacoTeclado.HeightRequest = 120;
        }
        void FechaTeclado(object sender, EventArgs e)
        {
            if (Device.OS == TargetPlatform.Android)
                EspacoTeclado.HeightRequest = 0;
        }
    }
}
