using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaXamarinMobile.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string FotoURL { get; set; }

        public int QuantidadePosts { get; set; }
        public int QuantidadeSeguindo { get; set; }
        public int QuantidadeSeguidores { get; set; }

        public bool Sigo { get; set; }
    }
}
