using Xamarin.Forms;

namespace InstaXamarinMobile
{
    public partial class LOADING : StackLayout
    {
        public LOADING()
        {
            InitializeComponent();

            FINALIZA();
        }

        public void INICIA()
        {
            INICIA("");
        }
        public void INICIA(string Texto)
        {
            this.IsEnabled = true;
            this.IsVisible = true;
            Loading.IsVisible = true;
            Loading.IsRunning = true;
            if (!string.IsNullOrEmpty(Texto))
            {
                LoadingTexto.IsVisible = true;
                LoadingTexto.IsEnabled = true;
                LoadingTexto.Text = Texto;
            }
            else
            {
                LoadingTexto.IsVisible = false;
                LoadingTexto.IsEnabled = false;
                LoadingTexto.Text = "";
            }
        }

        public void FINALIZA()
        {
            this.IsVisible = false;
            Loading.IsVisible = false;
            Loading.IsRunning = false;
            LoadingTexto.IsVisible = false;
            LoadingTexto.IsEnabled = false;
        }
    }
}

