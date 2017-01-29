using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaXamarinMobile.Models;
using RestSharp.Portable.HttpClient;
using System.Windows.Input;
using Xamarin.Forms;
using RestSharp.Portable;

namespace InstaXamarinMobile.ViewModels
{
    public class CadastroViewModel
    {
        public Usuario CADASTRO { get; set; }

        public CadastroViewModel(Usuario UsusarioAlteracao)
        {
            CADASTRO = UsusarioAlteracao;
        }

        public CadastroViewModel()
        {
            CADASTRO = new Usuario();
        }

        public async Task Cadastrar()
        {
            using (APIHelper API = new APIHelper())
            {
                App.UsuarioLogado = await API.POST<Usuario>("api/usuario", CADASTRO);

                if (App.Logado)
                {
                    Dictionary<String, String> Headers = API.HeadersAllRequests;
                    //Add Token
                    if (API.HeadersLastRequest.ContainsKey("token"))
                        Headers.Add("token", API.HeadersLastRequest["token"]);
                    API.HeadersAllRequests = Headers;//SET

                    MessagingCenter.Send<object>(this, "Logado");
                }
            }
        }

    }
}
