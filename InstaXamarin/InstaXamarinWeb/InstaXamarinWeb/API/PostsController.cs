using InstaXamarinWeb.Models;
using InstaXamarinWeb.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace InstaXamarinWeb.API
{
    public class PostsController : ApiController
    {
        private DB db = new DB();

        //Feed 
        [APIAutorizacao]
        [Route("api/posts/feed")]
        [HttpGet]
        public IHttpActionResult Feed(int Pagina, int QuantidadePagina)
        {
            int UsuariloLogado = Util.Utilitarios.GetTokenUsuarioLogado(Request);
            List<int> SEGUIDORES = db.Seguidores.Where(ss => ss.SeguidorID == UsuariloLogado).Select(ss => ss.SeguidoID).ToList();

            List<Post> POSTS = db.Posts
                .Where(pp => SEGUIDORES.Contains(pp.UsuarioId) && pp.Bloqueado == false)
                .OrderByDescending(pp => pp.Data)
                .Skip((Pagina - 1) * QuantidadePagina)
                .Take(QuantidadePagina).ToList();

            foreach (var P in POSTS)
                P.AtualizaDadosFoto(Utilitarios.GetLarguraTela(Request));

            return Ok(POSTS);
        }

        //Feed  Usuario
        [APIAutorizacao]
        [Route("api/posts/feed-usuario/{UsuarioID}")]
        [HttpGet]
        public IHttpActionResult FeedUsuario(int UsuarioID)
        {
            List<Post> POSTS = db.Posts
                .Where(pp => pp.UsuarioId == UsuarioID && pp.Bloqueado == false)
                .OrderByDescending(pp => pp.Data).ToList();

            foreach (var P in POSTS)
                P.AtualizaDadosFoto(Utilitarios.GetLarguraTela(Request));

            return Ok(POSTS);
        }


        //Get Foto
        [Route("api/posts/{PostID}/foto/{LarguraTela}")]
        [HttpGet]
        public HttpResponseMessage Foto(int PostID, int LarguraTela)
        {
            Post POST = db.Posts.Find(PostID);

            if (POST != null && POST.Foto != null)
            {
                POST.Foto = POST.Foto.RedimencionaProporcional(LarguraTela);

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StreamContent(new MemoryStream(POST.Foto));
                result.Content.Headers.ContentType = new MediaTypeHeaderValue(System.Net.Mime.MediaTypeNames.Image.Jpeg);
                return result;
            }

            return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
        }


        //Postar
        [APIAutorizacao]
        [Route("api/posts")]
        [HttpPost]
        public IHttpActionResult Postar(Post post)
        {
            post.Data = DateTime.Now;

            db.Posts.Add(post);

            Usuario Usuario = db.Usuarios.Find(post.UsuarioId);
            Usuario.QuantidadePosts++;

            db.SaveChanges();

            post.AtualizaDadosFoto(Utilitarios.GetLarguraTela(Request));

            return Ok(post);
        }

        //Exclui Post
        [APIAutorizacao]
        [Route("api/posts/{id}")]
        [HttpDelete]
        public IHttpActionResult DeletePost(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            Usuario Seguidor = db.Usuarios.Find(post.UsuarioId);
            Seguidor.QuantidadePosts--;

            db.Posts.Remove(post);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }




        //Curtir
        [APIAutorizacao]
        [Route("api/posts/curtir/{PostID}")]
        [HttpPut]
        public IHttpActionResult Curtir(int PostID, bool Curtir)
        {
            int UsuarioId = Util.Utilitarios.GetTokenUsuarioLogado(Request);

            PostInteracao POST_INTERACAO = db.PostsInteracoes
                .FirstOrDefault(pp => pp.PostId == PostID && pp.UsuarioId == UsuarioId && pp.Tipo == PostInteracao.TipoInteracao.Curtida);

            Post POST = db.Posts.Find(PostID);

            if (POST_INTERACAO == null)
            {
                if (Curtir)
                {
                    POST_INTERACAO = new PostInteracao();

                    POST_INTERACAO.PostId = PostID;
                    POST_INTERACAO.UsuarioId = Util.Utilitarios.GetTokenUsuarioLogado(Request);
                    POST_INTERACAO.Tipo = PostInteracao.TipoInteracao.Curtida;

                    POST_INTERACAO.Data = DateTime.Now;

                    db.PostsInteracoes.Add(POST_INTERACAO);
                    POST.QuantidadeCurtidas++;
                }
            }
            else
            {
                if (!Curtir)
                {
                    db.PostsInteracoes.Remove(POST_INTERACAO);
                    POST.QuantidadeCurtidas--;
                }
            }


            db.SaveChanges();

            return Ok();
        }

        //Lista Usuarios Curtidas
        [APIAutorizacao]
        [Route("api/posts/curtidas/{PostID}")]
        [HttpGet]
        public IHttpActionResult Curtidas(int PostID)
        {
            List<PostInteracao> POST_INTERACOES = db.PostsInteracoes
                .Where(pp => pp.PostId == PostID && pp.Tipo == PostInteracao.TipoInteracao.Curtida).ToList();

            return Ok(POST_INTERACOES);
        }


        //Comentar
        [APIAutorizacao]
        [Route("api/posts/comentar/{PostID}")]
        [HttpPut]
        public IHttpActionResult Comentar(int PostID, String Texto)
        {
            PostInteracao POST_INTERACAO = new PostInteracao();

            POST_INTERACAO.PostId = PostID;
            POST_INTERACAO.UsuarioId = Util.Utilitarios.GetTokenUsuarioLogado(Request);
            POST_INTERACAO.Tipo = PostInteracao.TipoInteracao.Comentario;

            POST_INTERACAO.Data = DateTime.Now;

            POST_INTERACAO.Texto = Texto;

            db.PostsInteracoes.Add(POST_INTERACAO);

            Post POST = db.Posts.Find(PostID);
            POST.QuantidadeComentarios++;

            db.SaveChanges();

            return Ok();
        }

        //Lista Comentarios
        [APIAutorizacao]
        [Route("api/posts/comentarios/{PostID}")]
        [HttpGet]
        public IHttpActionResult Comentarios(int PostID)
        {
            List<PostInteracao> POST_INTERACOES = db.PostsInteracoes
                .Where(pp => pp.PostId == PostID && pp.Tipo == PostInteracao.TipoInteracao.Comentario && pp.Bloqueado == false).ToList();

            return Ok(POST_INTERACOES);
        }


        //Denunciar
        [APIAutorizacao]
        [Route("api/posts/denunciar/{PostID}")]
        [HttpPut]
        public IHttpActionResult Denunciar(int PostID, [FromBody]String Texto)
        {
            PostInteracao POST_INTERACAO = new PostInteracao();

            POST_INTERACAO.PostId = PostID;
            POST_INTERACAO.UsuarioId = Util.Utilitarios.GetTokenUsuarioLogado(Request);
            POST_INTERACAO.Tipo = PostInteracao.TipoInteracao.DenunciaPost;
            POST_INTERACAO.Texto = Texto;

            POST_INTERACAO.Data = DateTime.Now;

            db.PostsInteracoes.Add(POST_INTERACAO);

            Post POST = db.Posts.Find(PostID);
            POST.QuantidadeDenuncias++;
            POST.Denunciado = true;

            if (POST.QuantidadeDenuncias > 3) //LIMITE DE DENUNCIAS
                POST.Bloqueado = true;

            db.SaveChanges();

            return Ok();
        }

        //Denunciar
        [APIAutorizacao]
        [Route("api/posts/denunciar-comentario/{ComentarioID}")]
        [HttpPut]
        public IHttpActionResult DenunciarComentario(int ComentarioID, [FromBody]String Texto)
        {
            PostInteracao POST_INTERACAO = new PostInteracao();

            POST_INTERACAO.ComentarioId = ComentarioID;
            POST_INTERACAO.UsuarioId = Util.Utilitarios.GetTokenUsuarioLogado(Request);
            POST_INTERACAO.Tipo = PostInteracao.TipoInteracao.DenunciaComentario;
            POST_INTERACAO.Texto = Texto;

            POST_INTERACAO.Data = DateTime.Now;

            db.PostsInteracoes.Add(POST_INTERACAO);


            PostInteracao COMENTARIO = db.PostsInteracoes.Find(ComentarioID);

            COMENTARIO.QuantidadeDenuncias++;
            COMENTARIO.Denunciado = true;

            if (COMENTARIO.QuantidadeDenuncias > 3) //LIMITE DE DENUNCIAS
                COMENTARIO.Bloqueado = true;


            db.SaveChanges();

            return Ok();

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostExists(int id)
        {
            return db.Posts.Count(e => e.Id == id) > 0;
        }
    }
}
