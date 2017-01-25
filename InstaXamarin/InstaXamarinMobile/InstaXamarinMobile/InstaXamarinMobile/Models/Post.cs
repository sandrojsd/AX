using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaXamarinMobile.Models
{
    public class Post
    {
        public int Id { get; set; }

        public DateTime Data { get; set; }

        public int UsuarioId { get; set; }

        public Usuario UsuarioDados { get; set; }

        public string FotoURL { get; set; }

        public string Descricao { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int QuantidadeCurtidas { get; set; }
        public int QuantidadeComentarios { get; set; }
        public int QuantidadeDenuncias { get; set; }

        public bool Denunciado { get; set; }

        public bool Bloqueado { get; set; }
    }
}
