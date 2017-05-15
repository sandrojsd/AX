using InstaXamarinMobile.Models;
using InstaXamarinMobile.Util;
using InstaXamarinMobile.Views;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;

namespace InstaXamarinMobile.ViewModels
{
    public class PostagemViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        public PostagemViewModel()
        {
            POST = new Post();
            Imagem = ImageSource.FromResource("InstaXamarinMobile.Imagens.Foto.png");

            TiraFotoCommand = new Command(async () => await TirarFoto(), () => true);
            BuscaFotoCommand = new Command(async () => await BuscarFoto(), () => true);
        }

        public Post POST { get; set; }

        public async Task<Post> Postar()
        {
            using (APIHelper API = new APIHelper())
            {
                POST.UsuarioId = App.UsuarioLogado.Id;

                POST.Latitude = App.Latitude;
                POST.Longitude = App.Longitude;

                Post PostGravado = await API.POST<Post>("api/posts", POST);

                //LIMPAR
                POST.Descricao = "";
                Imagem = ImageSource.FromResource("InstaXamarinMobile.Imagens.Foto.png");

                return PostGravado;
            }

        }


        private ImageSource _imagem;
        public ImageSource Imagem
        {
            get
            {
                return _imagem;
            }
            set
            {
                SetProperty(ref _imagem, value);
            }
        }
      

        public Command TiraFotoCommand { get; set; }
        public Command BuscaFotoCommand { get; set; }


        private async Task TirarFoto()
        {
            using (FotoHelper FOTO = new FotoHelper())
            {
                await FOTO.TirarFoto(1000, 1000);
                if (FOTO.FotoColetada)
                {
                    POST.Foto = FOTO.ImagemBytes;
                    Imagem = FOTO.GetImageSource();
                }
            }
        }

        private async Task BuscarFoto()
        {
            using (FotoHelper FOTO = new FotoHelper())
            {
                await FOTO.BuscarFoto(1000, 1000);
                if (FOTO.FotoColetada)
                {
                    POST.Foto = FOTO.ImagemBytes;
                    Imagem = FOTO.GetImageSource();
                }
            }
        }



    }
}
