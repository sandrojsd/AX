using InstaXamarinMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace InstaXamarinMobile.Views
{
    public partial class Cadastro : ContentPage
    {
        public Cadastro()
        {
            InitializeComponent();

            BindingContext = new CadastroViewModel();
        }

        public async void Cadastrar(object sender, EventArgs args)
        {
            try
            {
                //Valida campos
                if (String.IsNullOrEmpty(txtNome.Text))
                {
                    await DisplayAlert("Atenção", "Informe um nome.", "OK");
                    return;
                }

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

                LOAD.INICIA("Realizando Cadastro...");

                await ((CadastroViewModel)BindingContext).Cadastrar();
            }
            catch (HTTPException EX)
            {
                await DisplayAlert("Atenção", EX.Message, "OK");
            }
            catch
            {
                await DisplayAlert("Erro", "Ocorreu um erro. Tente novamente mais tarde", "OK");
            }
            finally
            {
                LOAD.FINALIZA();
            }
        }

        public async void Voltar(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }
    }
}
