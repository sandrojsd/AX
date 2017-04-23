using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaXamarinMobile.Models
{
    public class Usuario : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        string _FotoURL;
        public string FotoURL
        {
            get { return _FotoURL; }
            set
            {
                _FotoURL = value;
                OnPropertyChanged("FotoURL");
            }
        }


        public int QuantidadePosts { get; set; }
        public int QuantidadeSeguindo { get; set; }
        public int QuantidadeSeguidores { get; set; }

        public bool Meu { get; set; }
        public bool NaoMeu { get { return !Meu; } }

        bool _Sigo;
        public bool Sigo
        {
            get { return _Sigo; }
            set
            {
                _Sigo = value;
                OnPropertyChanged("Sigo");
                OnPropertyChanged("NaoSigo");
            }
        }

        public bool NaoSigo { get { return !Sigo; } }




        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
