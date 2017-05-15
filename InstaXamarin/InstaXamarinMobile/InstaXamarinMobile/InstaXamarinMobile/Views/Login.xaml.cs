using InstaXamarinMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace InstaXamarinMobile.Views
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();

            BindingContext = new LoginViewModel();

            this.btnEntrar.Clicked += async (sender, e) =>
            {
                try
                {
                    if (String.IsNullOrEmpty(txtEmail.Text))
                    {
                        await DisplayAlert("Atenção", "Informe um e-mail.", "OK");
                        return;
                    }

                    if (String.IsNullOrEmpty(txtSenha.Text))
                    {
                        await DisplayAlert("Atenção", "Informe uma senha.", "OK");
                        return;
                    }

                    LOAD.INICIA("Validando Usuário...");

                    await ((LoginViewModel)BindingContext).Entrar();
                }
                catch (HTTPException EX)
                {
                    await DisplayAlert("Atenção", EX.Message, "OK");
                }
                catch (Exception EX)
                {
                    await DisplayAlert("Erro", "Ocorreu um erro. Tente novamente mais tarde", "OK");
                }
                finally
                {
                    LOAD.FINALIZA();
                }
            };

            this.btnNovoUsuario.Clicked += async (sender, e) =>
            {
                await Navigation.PushModalAsync(new Cadastro());
            };


        }

        void AbreTeclado(object sender, EventArgs e)
        {
            if (Device.OS == TargetPlatform.Android)
                EspacoTeclado.HeightRequest = 200;
        }
        void FechaTeclado(object sender, EventArgs e)
        {
            if (Device.OS == TargetPlatform.Android)
                EspacoTeclado.HeightRequest = 0;
        }
    }
}