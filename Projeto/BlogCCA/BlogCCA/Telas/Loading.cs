using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlogCCA.Telas
{
    public class Loading : ContentPage
    {
        ActivityIndicator LOAD_ANIMACAO;
        Label TEXTO;

        public Loading()
        {
            LOAD_ANIMACAO = new ActivityIndicator();

            LOAD_ANIMACAO.VerticalOptions = LayoutOptions.EndAndExpand;
            LOAD_ANIMACAO.HorizontalOptions = LayoutOptions.FillAndExpand;

            
            
            TEXTO = new Label();
            TEXTO.BackgroundColor = Color.Red;
            TEXTO.TextColor = Color.White;
            TEXTO.VerticalOptions = LayoutOptions.StartAndExpand;
            TEXTO.HorizontalOptions = LayoutOptions.Center;

            //Conteudo da Page
            Content = new StackLayout
            {
                Children = { LOAD_ANIMACAO, TEXTO },
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,

                BackgroundColor = Color.FromHex(Device.OnPlatform("#b3FFFFFF", "#b3000000", "#b3000000"))
            };

            Finaliza();
        }

        public void Inicia()
        {
            Inicia("");
        }
        public void Inicia(String Texto)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                ((StackLayout)Content).IsVisible = true;
                LOAD_ANIMACAO.IsRunning = true;
                LOAD_ANIMACAO.IsVisible = true;

                if (!String.IsNullOrEmpty(Texto))
                {
                    LOAD_ANIMACAO.VerticalOptions = LayoutOptions.EndAndExpand;
                    TEXTO.IsVisible = true;
                    TEXTO.Text = Texto;
                }
                else
                {
                    LOAD_ANIMACAO.VerticalOptions = LayoutOptions.CenterAndExpand;
                }
            });
        }

        public void Finaliza()
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                ((StackLayout)Content).IsVisible = false;
                LOAD_ANIMACAO.IsRunning = false;
                LOAD_ANIMACAO.IsVisible = false;
                TEXTO.IsVisible = false;
            });
        }
    }
}
