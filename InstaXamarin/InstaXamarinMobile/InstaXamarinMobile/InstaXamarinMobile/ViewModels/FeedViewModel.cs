using InstaXamarinMobile.Models;
using InstaXamarinMobile.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InstaXamarinMobile.ViewModels
{
    public class FeedViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        ObservableCollection<Post> _POSTS;
        public ObservableCollection<Post> POSTS
        {
            get { return _POSTS; }
            set
            {
                _POSTS = value;
                OnPropertyChanged("POSTS");
            }
        }

        public int Pagina { get; set; }
        public int QuantidadePagina { get; set; }
        public bool SemMaisDados { get; set; }

        public FeedViewModel()
        {
            Pagina = 1;
            QuantidadePagina = 10;
            SemMaisDados = false;

            MessagingCenter.Subscribe<Object, Post>(this, "PostExcluido", (Sender, PostRemovido) => {
                var __POSTS = _POSTS;
                __POSTS.Remove(PostRemovido);
                POSTS = __POSTS;
            });
        }

        public async Task BuscaPostsCache()
        {
            //Alimanta com o Cache
            using (DBHelper DB = new DBHelper())
            {
                POSTS = new ObservableCollection<Post>(await DB.GetCache());
            }
        }

        public async Task BuscaPosts()
        {
            try
            {
                //await Task.Delay(3000); // Teste de loading

                using (APIHelper API = new APIHelper())
                {
                    API.HeadersRequest.Add("LarguraTela", App.LarguraTela.ToString());
                    var POSTSRetorno = await API.GET<ObservableCollection<Post>>("api/posts/feed/?Pagina=" + Pagina + "&QuantidadePagina=" + QuantidadePagina);

                    if (Pagina == 1)//Renova
                    {
                        POSTS = POSTSRetorno;

                        //Grava Cache
                        using (DBHelper DB = new DBHelper())
                        {
                            DB.SalvarTudo(POSTSRetorno.ToList());
                        }
                    }
                    else // Incrementa
                        foreach (var P in POSTSRetorno)
                            POSTS.Add(P);

                    if (POSTSRetorno.Count < QuantidadePagina)
                        SemMaisDados = true;
                }
            }
            catch (HTTPException EX)
            {
            }
            catch (Exception EX)
            {
            }
        }
    }
}
