using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections;

namespace BlogCCA.Telas
{
    public class ListaPosts : ContentPage
    {
        Entry TXTFiltro;

        ObservableCollection<Post> POSTS;
        ListView ListaPOSTS;

        Loading LOAD;

        public ListaPosts()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            Button btnMENU = new Button();
            btnMENU.BackgroundColor = Color.Transparent;
            btnMENU.Text = "Menu";
            btnMENU.HorizontalOptions = LayoutOptions.Start;
            btnMENU.Clicked += BtnMENU_Clicked;

            Button btnFiltrar = new Button();
            btnFiltrar.Text = "Buscar";
            btnFiltrar.BackgroundColor = Color.Transparent;
            btnFiltrar.HorizontalOptions = LayoutOptions.End;
            btnFiltrar.Clicked += BtnFiltrar_Clicked;

            TXTFiltro = new Entry();
            TXTFiltro.Placeholder = "Sua Busca...";
            TXTFiltro.HorizontalOptions = LayoutOptions.FillAndExpand;


            StackLayout BarraSuperior = new StackLayout {
                Children = {
                    btnMENU,
                    TXTFiltro,
                    btnFiltrar
                },
                Orientation = StackOrientation.Horizontal,
                HeightRequest = Device.OnPlatform(50, 50, 80)
            };



            //LISTA
            POSTS = new ObservableCollection<Post>();

            ListaPOSTS = new ListView();
            ListaPOSTS.ItemsSource = POSTS;
            ListaPOSTS.SeparatorColor = Color.FromHex("#FAFAFA");
            ListaPOSTS.HorizontalOptions = LayoutOptions.StartAndExpand;
            ListaPOSTS.RowHeight = 100;
            ListaPOSTS.ItemTemplate = new DataTemplate(() =>
            {
                Image IMG = new Image();
                IMG.HeightRequest = 100;
                IMG.WidthRequest = 100;
                IMG.SetBinding(Image.SourceProperty, "thumbnail");
                IMG.BackgroundColor = Color.White;
                IMG.HorizontalOptions = LayoutOptions.Start;

                Label TituloLabel = new Label();
                TituloLabel.SetBinding(Label.TextProperty, "title");
                TituloLabel.TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White);

                Label DataLabel = new Label();
                DataLabel.SetBinding(Label.TextProperty, "Data");
                DataLabel.TextColor = Device.OnPlatform(Color.FromHex("#666666"), Color.FromHex("#E9BD10"), Color.FromHex("#E9BD10"));


                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            IMG,
                            new StackLayout
                            {
                                Children =
                                {
                                    DataLabel,
                                    TituloLabel
                                }
                            }
                        },
                        
                    }
                };
            });
            ListaPOSTS.ItemTapped += ListaPOSTS_ItemTapped;
            ListaPOSTS.ItemAppearing += ListaPOSTS_ItemAppearing;


            Content = Util.ConteudoComLoading(new StackLayout {
                Children = {
                    BarraSuperior,
                    ListaPOSTS
                },
                Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
                
            }, ref LOAD);

            ListaUltimos();
        }

        enum Modo
        {
            Ultimos,
            Categoria,
            Favoritos,
        }
        Modo MODO;

        int Pagina;
        Categoria CategoriaFiltro;

        private async void ListaPOSTS_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var DADOS_LISTA = (IList)ListaPOSTS.ItemsSource;
            Post UltimoPost = (Post)POSTS[DADOS_LISTA.Count - 1];

            if (((Post)e.Item).id == UltimoPost.id)
            {
                TXTFiltro.Unfocus();
                Pagina++;

                LOAD.Inicia();

                List<Post> NovosPOSTS = new List<Post>();

                switch (MODO)
                {
                    case Modo.Ultimos:
                        NovosPOSTS = await WS.ListaPosts((TXTFiltro.Text != null ? TXTFiltro.Text : ""), 10, Pagina);
                        break;
                    case Modo.Categoria:
                        NovosPOSTS = await WS.ListaPostsPorCategoria(CategoriaFiltro.id, 10, Pagina);
                        break;
                    case Modo.Favoritos:
                    default:
                        break;
                }

                AtualizaLista(NovosPOSTS);

                LOAD.Finaliza();
            }
        }


        private void ListaPOSTS_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Post P = (Post)e.Item;
            ListaPOSTS.SelectedItem = null;

            App.NAV.PushAsync(new DetalhePost(P));
        }

        private async void BtnFiltrar_Clicked(object sender, EventArgs e)
        {
            TXTFiltro.Unfocus();
            await ListaUltimos();
        }

        private void BtnMENU_Clicked(object sender, EventArgs e)
        {
            MasterDetailPage MD = ((MasterDetailPage)((NavigationPage)Parent).Parent);
            bool Mostrando = MD.IsPresented;
            if (Mostrando)
                MD.IsPresented = false;
            else
                MD.IsPresented = true;
        }



        public async Task ListaUltimos()
        {
            LOAD.Inicia();

            MODO = Modo.Ultimos;

            LimpaLista();

            Pagina = 1;

            List<Post> ListaPOSTS = await WS.ListaPosts((TXTFiltro.Text != null ? TXTFiltro.Text : ""), 10, Pagina);
            AtualizaLista(ListaPOSTS);

            LOAD.Finaliza();
        }

        public async Task ListaFavoritos()
        {
            LOAD.Inicia();

            MODO = Modo.Favoritos;

            LimpaLista();

            Pagina = 1;

            await Task.Run(() => {
                DB.Inicializa();
                List<Post> ListaPOSTS = DB.ListaPostsFavoritos();
                DB.Finaliza();
                AtualizaLista(ListaPOSTS);
            });

            LOAD.Finaliza();

        }

        public async Task ListaPorCategoria(Categoria C)
        {
            LOAD.Inicia();

            MODO = Modo.Categoria;
            CategoriaFiltro = C;

            LimpaLista();

            Pagina = 1;

            List<Post> ListaPOSTS = await WS.ListaPostsPorCategoria(C.id, 10, Pagina);
            AtualizaLista(ListaPOSTS);

            LOAD.Finaliza();
        }

        void LimpaLista()
        {
            POSTS.Clear();
        }

        void AtualizaLista(List<Post> Posts)
        {
            if (Posts == null)
                return;

            foreach (var P in Posts)
                if(P.type == "post")
                    POSTS.Add(P);
        }
    }

}
