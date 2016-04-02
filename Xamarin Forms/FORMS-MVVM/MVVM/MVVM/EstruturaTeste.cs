using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVM
{
    public class EstruturaTeste : INotifyPropertyChanged
    {
        public EstruturaTeste()
        {
            this.AumentarIdade = new Command<string>((IdadeMais) =>
            {
                this.Idade += int.Parse(IdadeMais);
            });

            this.ReduzirIdade = new Command(() =>
            {
                this.Idade--;
            });
        }

        #region Propriedades

        string _nome;
        public string Nome
        {
            set
            {
                if (_nome != value)
                {
                    _nome = value;
                    OnPropertyChanged("Nome");
                }
            }
            get { return _nome; }
        }

        int _idade;
        public int Idade
        {
            set
            {
                if (_idade != value)
                {
                    _idade = value;
                    OnPropertyChanged("Idade");
                }
            }
            get { return _idade; }
        }

        #endregion

        #region Comandos

        public ICommand AumentarIdade { set; get; }

        public ICommand ReduzirIdade { set; get; }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
        }
    }
}
