using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InstaXamarinWeb.Models
{
    public class UsuarioToken
    {
        [Key]
        public int Id { get; set; }

        [Index]
        public int UsuarioId { get; set; }

        public enum Plataformas { Android = 0, iOS = 1 }

        [Index("Pesquisa", 1)]
        public Plataformas Plataforma { get; set; }

        [Index("Pesquisa", 2)]
        [StringLength(100)]
        public string Token { get; set; }

        public DateTime Data { get; set; }
    }
}