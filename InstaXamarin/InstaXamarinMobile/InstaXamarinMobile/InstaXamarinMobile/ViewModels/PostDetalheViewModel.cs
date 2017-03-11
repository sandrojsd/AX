using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaXamarinMobile.Models;
using Xamarin.Forms;
using System.ComponentModel;

namespace InstaXamarinMobile.ViewModels
{
    public class PostDetalheViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        Page PageParent;

        Post _POST;
        public Post POST
        {
            get { return _POST; }
            set
            {
                _POST = value;
                OnPropertyChanged("POST");
            }
        }

        #region Auxiliares

        public int QuantidadeCurtidas
        {
            get { return _POST.QuantidadeCurtidas; }
            set
            {
                _POST.QuantidadeCurtidas = value;
                OnPropertyChanged("QuantidadeCurtidas");
                OnPropertyChanged("IconeCurtida");
                OnPropertyChanged("IconeCurtidaCor");
            }
        }
        public string IconeCurtida
        { get { return _POST.IconeCurtida; } }
        public Color IconeCurtidaCor
        { get { return _POST.IconeCurtidaCor; } }

        public int QuantidadeComentarios
        {
            get { return _POST.QuantidadeComentarios; }
            set
            {
                _POST.QuantidadeComentarios = value;
                OnPropertyChanged("QuantidadeComentarios");
                OnPropertyChanged("IconeComentario");
                OnPropertyChanged("IconeComentarioCor");
            }
        }
        public string IconeComentario
        { get { return _POST.IconeComentario; } }
        public Color IconeComentarioCor
        { get { return _POST.IconeComentarioCor; } }
        #endregion

        List<PostInteracao> _COMENTARIOS;
        public List<PostInteracao> COMENTARIOS
        {
            get { return _COMENTARIOS; }
            set
            {
                _COMENTARIOS = value;
                OnPropertyChanged("COMENTARIOS");
            }
        }

        PostInteracao _NovoComentario;
        public PostInteracao NovoComentario
        {
            get { return _NovoComentario; }
            set
            {
                _NovoComentario = value;
                OnPropertyChanged("NovoComentario");
            }
        }


        public PostDetalheViewModel(Post P, Page Pg)
        {
            PageParent = Pg;
            POST = P;
            BuscaComentarios();

            NovoComentario = new PostInteracao();

            Curtir = new Command(async () => await _Curtir());
            Denunciar = new Command(async () => await _Denunciar());
            EnviarComentario = new Command(async () => await _EnviarComentario());
            Excluir = new Command(async () => await _Excluir());
        }

        public async void BuscaComentarios()
        {
            //Coleta Comentários
            try
            {
                using (APIHelper API = new APIHelper())
                {
                    COMENTARIOS = await API.GET<List<PostInteracao>>("api/posts/comentarios/" + POST.Id);
                    MessagingCenter.Send<Object>(this, "ComentariosRecebidos");
                }
            }
            catch (HTTPException EX)
            {
            }
            catch (Exception EX)
            {
            }
        }


        public Command Curtir { get; set; }
        public async Task _Curtir()
        {
            if (POST.EuCurti)
            {
                //Chama API
                try
                {
                    using (APIHelper API = new APIHelper())
                    {
                        await API.PUT("api/posts/curtir/" + POST.Id + "/?Curtir=false", null);
                        POST.EuCurti = false;
                        QuantidadeCurtidas--;
                    }
                }
                catch (HTTPException EX)
                {
                }
                catch (Exception EX)
                {
                }
            }
            else
            {
                //Chama API
                try
                {
                    using (APIHelper API = new APIHelper())
                    {
                        await API.PUT("api/posts/curtir/" + POST.Id + "/?Curtir=true", null);

                        POST.EuCurti = true;
                        QuantidadeCurtidas++;
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

        public Command Denunciar { get; set; }
        public async Task _Denunciar()
        {
            POST.EuDenunciei = true;

            //Chama API
            try
            {
                using (APIHelper API = new APIHelper())
                {
                    await API.PUT("api/posts/denunciar/" + POST.Id, null);
                }
            }
            catch (HTTPException EX)
            {
            }
            catch (Exception EX)
            {
            }
        }


        public Command EnviarComentario { get; set; }
        public async Task _EnviarComentario()
        {
            //NovoComentario
            NovoComentario.UsuarioId = App.UsuarioLogado.Id;
            NovoComentario.UsuarioDados = App.UsuarioLogado;
            NovoComentario.Data = DateTime.Now;

            NovoComentario.Meu = true;

            try
            {
                using (APIHelper API = new APIHelper())
                {
                    await API.PUT("api/posts/comentar/" + POST.Id + "/?texto=" + NovoComentario.Texto, null);
                }
            }
            catch (HTTPException EX)
            {
            }
            catch (Exception EX)
            {
            }

            COMENTARIOS.Add(NovoComentario);

            MessagingCenter.Send<Object>(this, "ComentariosRecebidos");

            POST.EuComentei = true;
            QuantidadeComentarios++;


            NovoComentario = new PostInteracao();
        }

        public async void DenunciarComentario(PostInteracao Comentario)
        {
            Comentario.EuDenunciei = true;

            try
            {
                using (APIHelper API = new APIHelper())
                {
                    await API.PUT("api/posts/denunciar-comentario/" + Comentario.Id, null);
                }
            }
            catch (HTTPException EX)
            {
            }
            catch (Exception EX)
            {
            }
        }


        public Command Excluir { get; set; }
        public async Task _Excluir()
        {
            var resposta = await PageParent.DisplayAlert("Certeza?", "Quer mesmo excluir essa foto?", "Sim", "Não");
            if (resposta)
            {
                try
                {
                    using (APIHelper API = new APIHelper())
                    {
                        await API.DELETE("api/posts/" + POST.Id);
                        MessagingCenter.Send<Object, Post>(this, "PostExcluido", POST);
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
}
