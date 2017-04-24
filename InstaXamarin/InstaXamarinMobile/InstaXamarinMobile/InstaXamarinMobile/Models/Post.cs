using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InstaXamarinMobile.Models
{
    public class Post : INotifyPropertyChanged
    {
        [PrimaryKey]
        public int Id { get; set; }

        public DateTime Data { get; set; }

        public int UsuarioId { get; set; }

        [Ignore]
        public Usuario UsuarioDados { get; set; }

        public String _UsuarioDados { get; set; }
        public void SerializaUsuarioDados()
        {
            if (UsuarioDados != null)
                _UsuarioDados = JsonConvert.SerializeObject(UsuarioDados);
        }
        public void DesSerializaUsuarioDados()
        {
            if (!String.IsNullOrEmpty(_UsuarioDados))
                UsuarioDados = JsonConvert.DeserializeObject<Usuario>(_UsuarioDados);
        }

        public string FotoURL { get; set; }

        [Ignore]
        public byte[] Foto { get; set; }

        public string _Descricao;
        public string Descricao
        {
            get { return _Descricao; }
            set
            {
                _Descricao = value;
                OnPropertyChanged("Descricao");
            }
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int _QuantidadeCurtidas;
        public int QuantidadeCurtidas
        {
            get { return _QuantidadeCurtidas; }
            set
            {
                _QuantidadeCurtidas = value;
                OnPropertyChanged("QuantidadeCurtidas");
                OnPropertyChanged("IconeCurtida");
                OnPropertyChanged("IconeCurtidaCor");
            }
        }

        public int _QuantidadeComentarios;
        public int QuantidadeComentarios
        {
            get { return _QuantidadeComentarios; }
            set
            {
                _QuantidadeComentarios = value;
                OnPropertyChanged("QuantidadeComentarios");
                OnPropertyChanged("IconeComentario");
                OnPropertyChanged("IconeComentarioCor");
            }
        }

        public int QuantidadeDenuncias { get; set; }

        public bool Denunciado { get; set; }

        public bool Bloqueado { get; set; }

        public bool Meu { get; set; }
        public bool NaoMeu { get { return !Meu; } }

        public bool EuCurti { get; set; }
        public bool EuComentei { get; set; }
        public bool EuDenunciei { get; set; }

        [Ignore]
        public string IconeCurtida { get { return (EuCurti ? "fa-heart" : "fa-heart-o"); } }
        [Ignore]
        public Color IconeCurtidaCor { get { return (EuCurti ? Color.Green : Color.Blue); } }

        [Ignore]
        public string IconeComentario { get { return (EuComentei ? "fa-comment" : "fa-comment-o"); } }
        [Ignore]
        public Color IconeComentarioCor { get { return (EuComentei ? Color.Green : Color.Blue); } }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
