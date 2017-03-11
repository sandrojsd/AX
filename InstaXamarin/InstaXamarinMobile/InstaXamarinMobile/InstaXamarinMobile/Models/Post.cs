using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InstaXamarinMobile.Models
{
    public class Post
    {
        public int Id { get; set; }

        public DateTime Data { get; set; }

        public int UsuarioId { get; set; }

        public Usuario UsuarioDados { get; set; }

        public string FotoURL { get; set; }
        public byte[] Foto { get; set; }

        public string Descricao { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int QuantidadeCurtidas { get; set; }
        public int QuantidadeComentarios { get; set; }
        public int QuantidadeDenuncias { get; set; }

        public bool Denunciado { get; set; }

        public bool Bloqueado { get; set; }

        public bool Meu { get; set; }
        public bool NaoMeu { get { return !Meu; } }

        public bool EuCurti { get; set; }
        public bool EuComentei { get; set; }
        public bool EuDenunciei { get; set; }

        public string IconeCurtida { get { return (EuCurti ? "fa-heart" : "fa-heart-o"); } }
        public Color IconeCurtidaCor { get { return (EuCurti ? Color.Green : Color.Blue); } }

        public string IconeComentario { get { return (EuComentei ? "fa-comment" : "fa-comment-o"); } }
        public Color IconeComentarioCor { get { return (EuComentei ? Color.Green : Color.Blue); } }

    }
}
