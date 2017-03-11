using InstaXamarinMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace InstaXamarinMobile.Views
{
    public partial class ComentarioParcial : StackLayout
    {
        public delegate void DenunciarComentario(PostInteracao comentario);
        public event DenunciarComentario OnDenunciaComentario;

        PostInteracao _COMENTARIO;

        public ComentarioParcial(PostInteracao comentario)
        {
            _COMENTARIO = comentario;

            InitializeComponent();

            BindingContext = _COMENTARIO;
        }

        public void Denunciar(object sender, EventArgs e)
        {
            if (OnDenunciaComentario != null)
                OnDenunciaComentario(_COMENTARIO);
        }
    }
}
