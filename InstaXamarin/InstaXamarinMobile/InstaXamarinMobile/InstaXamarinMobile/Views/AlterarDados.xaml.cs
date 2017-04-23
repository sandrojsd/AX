using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Rg.Plugins.Popup.Extensions;
using InstaXamarinMobile.Models;

namespace InstaXamarinMobile.Views
{
    public partial class AlterarDados : PopupPage
    {
        public delegate void UsuarioAlterado(Usuario usarioAlterado);
        public event UsuarioAlterado OnUsuarioAlterado;

        Usuario USUARIO;

        public AlterarDados(Usuario usarioAlterado)
        {
            USUARIO = usarioAlterado;
            USUARIO.Senha = "";
            BindingContext = USUARIO;
            InitializeComponent();
            btnFechar.Clicked += BtnFechar_Clicked;
            btnAlterar.Clicked += BtnAlterar_Clicked;
        }

        private void BtnFechar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAllPopupAsync();
        }

        private void BtnAlterar_Clicked(object sender, EventArgs e)
        {
            if (OnUsuarioAlterado != null)
                OnUsuarioAlterado(USUARIO);

            Navigation.PopAllPopupAsync();
        }

    }


}
