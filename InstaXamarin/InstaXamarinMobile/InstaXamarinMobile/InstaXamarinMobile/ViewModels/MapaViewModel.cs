using InstaXamarinMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InstaXamarinMobile.ViewModels
{
    public class MapaViewModel
    {
        public List<Post> POSTS { get; set; }

        public MapaViewModel()
        {
            MessagingCenter.Subscribe<Object, Post>(this, "PostExcluido", (Sender, PostRemovido) => {
                if(POSTS!=null)
                    POSTS.Remove(PostRemovido);
            });
        }

        public async Task BuscaPosts()
        {
            try
            {
                using (APIHelper API = new APIHelper())
                {
                    API.HeadersRequest.Add("LarguraTela", App.LarguraTela.ToString());
                    POSTS = await API.GET<List<Post>>("api/posts/feed/mapa");
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
