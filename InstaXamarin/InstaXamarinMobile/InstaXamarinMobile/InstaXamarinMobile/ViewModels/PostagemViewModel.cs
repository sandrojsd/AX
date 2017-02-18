using InstaXamarinMobile.Models;
using InstaXamarinMobile.Views;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Geolocation;
using XLabs.Platform.Services.Media;

namespace InstaXamarinMobile.ViewModels
{
    public class PostagemViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        public PostagemViewModel()
        {
            ConfigurarMedia();

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

                POST.Latitude = Latitude;
                POST.Longitude = Longitude;


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

        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                SetProperty(ref _status, value);
            }
        }

        public Command TiraFotoCommand { get; set; }
        public Command BuscaFotoCommand { get; set; }

        public Double Latitude { get; set; }
        public Double Longitude { get; set; }


        private async Task<MediaFile> TirarFoto()
        {
            ConfigurarMedia();

            return await _mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions { DefaultCamera = CameraDevice.Rear, MaxPixelDimension = 1000 }).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Status = t.Exception.InnerException.ToString();
                }
                else if (t.IsCanceled)
                {
                    Status = "Cancelou";
                }
                else
                {
                    var mediaFile = t.Result;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        mediaFile.Source.CopyTo(ms);
                        POST.Foto = ms.ToArray();
                    }

                    Imagem = ImageSource.FromStream(() => mediaFile.Source);

                    return mediaFile;
                }

                return null;
            }, _scheduler);
        }

        private async Task BuscarFoto()
        {
            ConfigurarMedia();

            try
            {
                var mediaFile = await _mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
                {
                    DefaultCamera = CameraDevice.Rear,
                    MaxPixelDimension = 1000
                });

                using (MemoryStream ms = new MemoryStream())
                {
                    mediaFile.Source.CopyTo(ms);
                    POST.Foto = ms.ToArray();
                }

                Imagem = ImageSource.FromStream(() => mediaFile.Source);
            }
            catch (System.Exception ex)
            {
                Status = ex.Message;
            }
        }


        public void ColetaPosicao()
        {
            ConfigurarGPS();

            cancelSource = new CancellationTokenSource();

            Latitude = 0;
            Longitude = 0;

            PositionStatus = String.Empty;

            this.geolocator.GetPositionAsync(timeout: 10000, cancelToken: this.cancelSource.Token, includeHeading: true)
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                        PositionStatus = ((GeolocationException)t.Exception.InnerException).Error.ToString();
                    else if (t.IsCanceled)
                        PositionStatus = "Cancelado";
                    else
                    {
                        PositionStatus = t.Result.Timestamp.ToString("G");
                        Latitude = t.Result.Latitude;
                        Longitude = t.Result.Longitude;
                    }

                }, _scheduler);
        }


        #region Auxiliares

        String PositionStatus;
        CancellationTokenSource cancelSource;

        IGeolocator geolocator;

        void ConfigurarGPS()
        {
            if (geolocator != null)
                return;
            geolocator = DependencyService.Get<IGeolocator>() ?? Resolver.Resolve<IGeolocator>();
        }


        private void ConfigurarMedia()
        {
            if (_mediaPicker != null)
                return;

            var device = Resolver.Resolve<IDevice>();

            _mediaPicker = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;
        }

        private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();

        private IMediaPicker _mediaPicker;

        private static double ConverteBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        #endregion

    }
}
