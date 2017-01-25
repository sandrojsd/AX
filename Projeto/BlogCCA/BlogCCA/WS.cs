using RestSharp.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCCA
{
    public class WS
    {
        public static async Task<List<Categoria>> ListaCategorias()
        {
            var client = new RestClient("http://comocriaraplicativos.com.br/");

            var request = new RestRequest("?json={Funcao}", Method.GET);
            request.AddUrlSegment("Funcao", "get_category_index");

            RestResponse<RETORNO_CATEGORIA> response = (RestResponse<RETORNO_CATEGORIA>)await client.Execute<RETORNO_CATEGORIA>(request);

            if (response != null && response.Data!=null)
                return response.Data.categories;
            else
                return null;
        }

        public static async Task<List<Post>> ListaPosts(String pesquisa, int Quantidade, int Pagina)
        {
            var client = new RestClient("http://comocriaraplicativos.com.br/");

            RestRequest request = new RestRequest("?json={Funcao}&s={Pesquisa}&count={Quantos}&page={Pagina}", Method.GET);
            if(String.IsNullOrEmpty(pesquisa))
                request = new RestRequest("?json={Funcao}&count={Quantos}&page={Pagina}", Method.GET);

            request.AddUrlSegment("Funcao", "get_posts");
            request.AddUrlSegment("Pesquisa", pesquisa);
            request.AddUrlSegment("Quantos", Quantidade);
            request.AddUrlSegment("Pagina", Pagina);


            RestResponse<RETORNO_POSTS> response = (RestResponse<RETORNO_POSTS>)await client.Execute<RETORNO_POSTS>(request);

            if (response != null && response.Data != null)
                return response.Data.posts;
            else
                return null;
        }

        public static async Task<List<Post>> ListaPostsPorCategoria(int IdCategoria, int Quantidade, int Pagina)
        {
            var client = new RestClient("http://comocriaraplicativos.com.br/");

            var request = new RestRequest("?json={Funcao}&id={IDCategoria}&count={Quantos}&page={Pagina}", Method.GET);
            request.AddUrlSegment("Funcao", "get_category_posts");
            request.AddUrlSegment("IDCategoria", IdCategoria);
            request.AddUrlSegment("Quantos", Quantidade);
            request.AddUrlSegment("Pagina", Pagina);

            RestResponse<RETORNO_POSTS> response = (RestResponse<RETORNO_POSTS>)await client.Execute<RETORNO_POSTS>(request);

            if (response != null && response.Data != null)
                return response.Data.posts;
            else
                return null;
        }
    }
}
