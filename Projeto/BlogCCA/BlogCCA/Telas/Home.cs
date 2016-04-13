using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlogCCA.Telas
{
    public class Home : MasterDetailPage
    {
        Menu MENU;
        ListaPosts LISTA;
        public Home()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            MENU = new Menu();
            MENU.OnSelecionaTodos += MENU_OnSelecionaTodos;
            MENU.OnSelecionaCategoria += MENU_OnSelecionaCategoria;
            MENU.OnSelecionaFavoritos += MENU_OnSelecionaFavoritos;

            LISTA = new ListaPosts();

            Master = MENU;

            App.NAV = new NavigationPage();

            Detail = App.NAV;
            App.NAV.PushAsync(LISTA);
        }

        private async void MENU_OnSelecionaTodos()
        {
            IsPresented = false;
            await LISTA.ListaUltimos();
        }

        private async void MENU_OnSelecionaFavoritos()
        {
            IsPresented = false;
            await LISTA.ListaFavoritos();
        }

        private async void MENU_OnSelecionaCategoria(Categoria C)
        {
            IsPresented = false;
            await LISTA.ListaPorCategoria(C);
        }
    }
}
