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
using System.Collections.ObjectModel;
using System.ComponentModel;
using XLabs.Platform.Services.Media;
using System.IO;
using XLabs.Ioc;
using XLabs.Platform.Device;
using InstaXamarinMobile.Custom;

namespace InstaXamarinMobile.ViewModels
{
    public class PerfilViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        Usuario _USUARIO;
        public Usuario USUARIO
        {
            get { return _USUARIO; }
            set
            {
                _USUARIO = value;
                OnPropertyChanged("USUARIO");
            }
        }

        public int IdUsuario { get; set; }

        public List<Post> POSTS { get; set; }


        public PerfilViewModel(int IdUsuario)
        {
            this.IdUsuario = IdUsuario;
        }


        public async Task BuscaDadosUsuario()
        {
            try
            {
                using (APIHelper API = new APIHelper())
                {
                    API.HeadersRequest.Add("LarguraTela", App.LarguraTela.ToString());
                    USUARIO = await API.GET<Usuario>("api/usuario/" + IdUsuario);

                    MessagingCenter.Send<Object>(this, "DadosUsuarioAtualizados");
                }
            }
            catch (HTTPException EX)
            {
            }
            catch (Exception EX)
            {
            }
        }

        public async Task BuscaPostsUsuario()
        {
            try
            {
                using (APIHelper API = new APIHelper())
                {
                    API.HeadersRequest.Add("LarguraTela", App.LarguraTela.ToString());
                    POSTS = await API.GET<List<Post>>("api/posts/feed-usuario/" + IdUsuario);

                    MessagingCenter.Send<Object>(this, "PostsUsuarioAtualizados");
                }
            }
            catch (HTTPException EX)
            {
            }
            catch (Exception EX)
            {
            }
        }

        //Seguir
        public async Task SeguirUsuario()
        {
            try
            {
                using (APIHelper API = new APIHelper())
                {
                    await API.PUT("api/usuario/seguir/" + IdUsuario, null);
                }

                USUARIO.Sigo = true;
            }
            catch (HTTPException EX)
            {
            }
            catch (Exception EX)
            {
            }
        }

        //Deixar de seguir
        public async Task DeixarSeguirUsuario()
        {
            try
            {
                using (APIHelper API = new APIHelper())
                {
                    await API.PUT("api/usuario/deixar-seguir/" + IdUsuario, null);
                }

                USUARIO.Sigo = false;
            }
            catch (HTTPException EX)
            {
            }
            catch (Exception EX)
            {
            }
        }

        //Alterar Dados
        public async Task AlterarDados(Usuario UsuarioDados)
        {
            try
            {
                App.UsuarioLogado = UsuarioDados;

                using (APIHelper API = new APIHelper())
                {
                    await API.PUT("api/usuario/", UsuarioDados);
                }

            }
            catch (HTTPException EX)
            {
            }
            catch (Exception EX)
            {
            }
        }


        //Alterar Dados
        public async Task CancelarConta()
        {
            try
            {
                using (APIHelper API = new APIHelper())
                {
                    await API.DELETE("api/usuario/");
                }
            }
            catch (HTTPException EX)
            {
            }
            catch (Exception EX)
            {
            }
        }




        //Trocar Foto
        public async Task TrocarFoto(byte[] FotoNova)
        {
            try
            {
                //Atualiza na API
                using (APIHelper API = new APIHelper())
                {
                    //Passar Dados 
                    USUARIO.FotoURL = await API.PUT<String>("api/usuario/foto", FotoNova);
                    App.UsuarioLogado.FotoURL = USUARIO.FotoURL;
                }
            }
            catch (HTTPException EX)
            {
            }
            catch (Exception EX)
            {
            }
        }

    }
}