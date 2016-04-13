using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlogCCA.Telas
{
    public class Menu : ContentPage
    {
        ListView ListaCategorias;
        Button BtnFavoritos;
        Button BtnTodos;

        Loading LOAD;

        List<Categoria> CategoriasDados;

        public Menu()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            Title = "Menu";

            ListaCategorias = new ListView();
            ListaCategorias.ItemsSource = CategoriasDados;
            ListaCategorias.BackgroundColor = Color.FromHex("#666666");
            ListaCategorias.SeparatorColor = Color.Transparent;
            ListaCategorias.ItemTemplate = new DataTemplate(() =>
            {
                Label NomeLabel = new Label();
                NomeLabel.SetBinding(Label.TextProperty, "title");
                NomeLabel.TextColor = Color.White;

                Label QuantidadeLabel = new Label();
                QuantidadeLabel.SetBinding(Label.TextProperty, "quantidade");
                QuantidadeLabel.TextColor = Color.Lime;

                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Padding = new Thickness(30, 10, 0, 10),
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            NomeLabel,
                            QuantidadeLabel
                        }
                    }
                };
            });
            ListaCategorias.ItemTapped += ListaCategorias_ItemTapped;


            BtnTodos = new Button();
            BtnTodos.BackgroundColor = Color.FromHex("#D56F00");
            BtnTodos.TextColor = Color.FromHex("#FFFFFF");
            BtnTodos.Text = "Últimos Posts";
            BtnTodos.WidthRequest = 200;
            BtnTodos.HorizontalOptions = LayoutOptions.CenterAndExpand;
            BtnTodos.Clicked += BtnTodos_Clicked;

            BtnFavoritos = new Button();
            BtnFavoritos.BackgroundColor = Color.FromHex("#E9BD10");
            BtnFavoritos.TextColor = Color.FromHex("#666666");
            BtnFavoritos.Text = "Favoritos";
            BtnFavoritos.WidthRequest = 200;
            BtnFavoritos.HorizontalOptions = LayoutOptions.CenterAndExpand;
            BtnFavoritos.Clicked += BtnFavoritos_Clicked;

            Content = new StackLayout
            {
                Children = {
                    Util.ConteudoComLoading(
                        new StackLayout
                        {
                            Children = {
                                new Image
                                {
                                    Source = ImageSource.FromFile("Logo.png"),
                                    WidthRequest = 50,
                                    HeightRequest = 80,
                                    HorizontalOptions = LayoutOptions.Center,
                                    VerticalOptions = LayoutOptions.Start
                                },
                                BtnTodos,
                                BtnFavoritos,
                                ListaCategorias
                            },
                            BackgroundColor = Color.FromHex("#FAFAFA")
                        }, ref LOAD)
                },
                Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
                BackgroundColor = Color.FromHex("#FAFAFA")
            };

            BuscaCategorias();
        }

        private void BtnTodos_Clicked(object sender, EventArgs e)
        {
            ListaCategorias.SelectedItem = null;

            if (OnSelecionaTodos != null)
                OnSelecionaTodos();
        }

        private void BtnFavoritos_Clicked(object sender, EventArgs e)
        {
            ListaCategorias.SelectedItem = null;

            if (OnSelecionaFavoritos != null)
                OnSelecionaFavoritos();
        }

        private void ListaCategorias_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Categoria C = (Categoria)e.Item;

            if (OnSelecionaCategoria != null)
                OnSelecionaCategoria(C);


        }

        async void BuscaCategorias()
        {
            //Cats Em Cache
            DB.Inicializa();
            List<Categoria> CATS_CACHE = DB.ListaCategorias();
            DB.Finaliza();

            AtualizaLstaCategorias(CATS_CACHE);

            if (CATS_CACHE.Count == 0)
                LOAD.Inicia();

            List<Categoria> CATS = await WS.ListaCategorias();

            DB.Inicializa();
            DB.AtualizaCategorias(CATS);
            DB.Finaliza();

            AtualizaLstaCategorias(CATS);

            if (CATS_CACHE.Count == 0)
                LOAD.Finaliza();
        }

        void AtualizaLstaCategorias(List<Categoria> CATS)
        {
            CategoriasDados = CATS;
            ListaCategorias.ItemsSource = CategoriasDados;
        }

        public delegate void SelecionaTodos();
        public event SelecionaTodos OnSelecionaTodos;

        public delegate void SelecionaCategoria(Categoria C);
        public event SelecionaCategoria OnSelecionaCategoria;

        public delegate void SelecionaFavoritos();
        public event SelecionaFavoritos OnSelecionaFavoritos;

    }
}
