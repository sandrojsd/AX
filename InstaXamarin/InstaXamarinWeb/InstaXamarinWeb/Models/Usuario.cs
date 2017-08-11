using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InstaXamarinWeb.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }


        [Index("Login", Order = -1)]
        public string Email { get; set; }

        [Index("Login", Order = -1)]
        [JsonIgnore]
        public string Senha { get; set; }

        [Index("Login", Order = -1)]
        [JsonIgnore]
        public bool Bloqueado { get; set; }

        [JsonIgnore]
        public byte[] Foto { get; set; }

        [NotMapped]
        public string FotoURL
        {
            get
            {
                return (Foto != null ? Util.Utilitarios.GET_URL("api/usuario/" + Id + "/foto?versao=" + FotoVersao) : "logo.png");
            }
        }

        [NotMapped]
        [JsonIgnore]
        public string FotoVersao
        {
            get
            {
                if (FotoDataAtualizacao != null && FotoDataAtualizacao.Value != DateTime.MinValue)
                    return FotoDataAtualizacao.Value.ToString("ddMMyyyyHHmmss");
                else
                    return "1";
            }
        }

        [JsonIgnore]
        public DateTime? FotoDataAtualizacao { get; set; }

        public int QuantidadePosts { get; set; }
        public int QuantidadeSeguindo { get; set; }
        public int QuantidadeSeguidores { get; set; }

        [JsonIgnore]
        public DateTime DataCadastro { get; set; }

        [NotMapped]
        public bool Sigo { get; set; }

        [NotMapped]
        public bool Meu { get; set; }

    }
}