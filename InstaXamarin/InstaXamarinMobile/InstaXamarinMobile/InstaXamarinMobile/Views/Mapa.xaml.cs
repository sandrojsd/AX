using InstaXamarinMobile.Custom;
using InstaXamarinMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace InstaXamarinMobile.Views
{
    public partial class Mapa : ContentPage
    {
        MapaViewModel MVM;
        bool PostDetalhe = false;

        public Mapa()
        {
            InitializeComponent();
            MVM = new MapaViewModel();

            //Ponto central padrão - São Paulo
            MAPA.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(-23.547778, -46.635833), Distance.FromKilometers(500)));

            MessagingCenter.Subscribe<Object>(this, "LocalizacaoEncontrada", (Sender) => {
                //Ponto central e Raio de visão
                MAPA.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(App.Latitude, App.Longitude), Distance.FromKilometers(5)));
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await MVM.BuscaPosts();

            //Adiciona os Pinos
            MAPA.PinosFotos = new List<PinoFoto>();
            foreach (var P in MVM.POSTS)
            {
                PinoFoto PF = new PinoFoto();
                PF.POST = P;
                PF.OnClicaPost += Pin_OnClicaPost;

                MAPA.PinosFotos.Add(PF);
            }

            //Atualiza Mapa
            if (App.Localizado && !PostDetalhe)
            {
                PostDetalhe = false;
                MAPA.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(App.Latitude, App.Longitude), Distance.FromKilometers(5)));
            }

        }

        private void Pin_OnClicaPost(Models.Post post)
        {
            PostDetalhe = true;
            Navigation.PushAsync(new PostDetalhado(post));
        }
    }
}
