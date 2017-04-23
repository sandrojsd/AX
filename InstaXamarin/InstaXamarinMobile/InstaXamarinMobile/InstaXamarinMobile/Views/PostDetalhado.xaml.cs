using InstaXamarinMobile.Models;
using InstaXamarinMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace InstaXamarinMobile.Views
{
    public partial class PostDetalhado : ContentPage
    {
        PostDetalheViewModel PDVM { get; set; }

        public PostDetalhado(Post P)
        {
            InitializeComponent();

            Title = "Foto de " + P.UsuarioDados.Nome;

            PDVM = new PostDetalheViewModel(P, this);

            BindingContext = PDVM;

            MessagingCenter.Subscribe<Object>(this, "ComentariosRecebidos", (sender) => {

                Comentarios.Children.Clear();

                foreach (var C in PDVM.COMENTARIOS)
                {
                    ComentarioParcial CP = new ComentarioParcial(C);
                    CP.OnDenunciaComentario += CP_OnDenunciaComentario;

                    Comentarios.Children.Add(CP);
                }
            });

            MessagingCenter.Subscribe<Object, Post>(this, "PostExcluido", async (Sender, PostRemovido) => {
                await Navigation.PopAsync();
            });
        }

        private void CP_OnDenunciaComentario(PostInteracao comentario)
        {
            PDVM.DenunciarComentario(comentario);
        }

        private void ClickUsuario(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Perfil(PDVM.POST.UsuarioId));
        }
    }
}
