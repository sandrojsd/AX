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

namespace InstaXamarinMobile.ViewModels
{
    public class ListaUsuariosParcialViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        ObservableCollection<Usuario> _USUARIOS;
        public ObservableCollection<Usuario> USUARIOS
        {
            get { return _USUARIOS; }
            set
            {
                _USUARIOS = value;
                OnPropertyChanged("USUARIOS");
            }
        }


        public ListaUsuariosParcialViewModel()
        {
            _SeguirUsuarioCommand = new Command(SeguirUsuario);
            _DeixarSeguirUsuarioCommand = new Command(DeixarSeguirUsuario);
        }


        public async Task BuscaUsuarios(String Termo)
        {
            try
            {
                using (APIHelper API = new APIHelper())
                {
                    API.HeadersRequest.Add("LarguraTela", App.LarguraTela.ToString());
                    USUARIOS = await API.GET<ObservableCollection<Usuario>>("api/usuario/pesquisa/?termo=" + Termo);
                }
            }
            catch (HTTPException EX)
            {
            }
            catch (Exception EX)
            {
            }
        }

        public async Task BuscaSeguidores(int IDSeguido)
        {
            try
            {
                using (APIHelper API = new APIHelper())
                {
                    API.HeadersRequest.Add("LarguraTela", App.LarguraTela.ToString());
                    USUARIOS = await API.GET<ObservableCollection<Usuario>>("api/usuario/" + IDSeguido + "/seguidores/");
                }
            }
            catch (HTTPException EX)
            {
            }
            catch (Exception EX)
            {
            }
        }

        public async Task BuscaSeguidos(int IDSeguidor)
        {
            try
            {
                using (APIHelper API = new APIHelper())
                {
                    API.HeadersRequest.Add("LarguraTela", App.LarguraTela.ToString());
                    USUARIOS = await API.GET<ObservableCollection<Usuario>>("api/usuario/" + IDSeguidor + "/seguidos/");
                }
            }
            catch (HTTPException EX)
            {
            }
            catch (Exception EX)
            {
            }

        }


        ICommand _SeguirUsuarioCommand;
        public ICommand SeguirUsuarioCommand
        {
            get { return _SeguirUsuarioCommand; }
        }
        async void SeguirUsuario(object s)
        {
            int IdUsuario = (int)s;
            try
            {
                using (APIHelper API = new APIHelper())
                {
                    await API.PUT("api/usuario/seguir/" + IdUsuario, null);
                }

                USUARIOS.First(uu => uu.Id == IdUsuario).Sigo = true;
            }
            catch (HTTPException EX)
            {
            }
            catch (Exception EX)
            {
            }
        }

        ICommand _DeixarSeguirUsuarioCommand;
        public ICommand DeixarSeguirUsuarioCommand
        {
            get { return _DeixarSeguirUsuarioCommand; }
        }
        async void DeixarSeguirUsuario(object s)
        {
            int IdUsuario = (int)s;
            try
            {
                using (APIHelper API = new APIHelper())
                {
                    await API.PUT("api/usuario/deixar-seguir/" + IdUsuario, null);
                }

                USUARIOS.First(uu => uu.Id == IdUsuario).Sigo = false;
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
