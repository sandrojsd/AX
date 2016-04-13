using SQLite;
using SQLite.Net;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlogCCA
{
    public class DB
    {
        static SQLiteConnection CONN;

        public static void Inicializa()
        {
            if (CONN == null)
            {
                CONN = new SQLiteConnection(DependencyService.Get<iRecursosNativos>().SQLitePlataform, DependencyService.Get<iRecursosNativos>().GetCaminho("OFFLINE.DB"));

                Debug.WriteLine("File: " + DependencyService.Get<iRecursosNativos>().GetCaminho("OFFLINE.DB"));

                //CONN.CreateTable<Post>();

                CONN.CreateTable<Categoria>();

                CONN.CreateTable<Post>();

                CONN.CreateTable<CategoriaPost>();
                CONN.CreateTable<Tag>();
                CONN.CreateTable<Author>();
                CONN.CreateTable<Comentario>();
            }
        }

        public static void Finaliza()
        {
            if (CONN != null)
            {
                CONN.Close();
                CONN.Dispose();
                CONN = null;
            }
        }

        #region Categorias

        public static void AtualizaCategorias(List<Categoria> CATs)
        {
            foreach (var C in CATs)
            {
                RemoveCategoria(C);
                InsereCategoria(C);
            }
        }

        static void InsereCategoria(Categoria C)
        {
            if (!CategoriaExiste(C))
                CONN.Insert(C);
        }

        static void RemoveCategoria(Categoria C)
        {
            if (CategoriaExiste(C))
                CONN.Delete(C);
        }

        static bool CategoriaExiste(Categoria C)
        {
            int quantidade = CONN.ExecuteScalar<int>("Select count(*) from Categoria where id=?", C.id);

            if (quantidade > 0)
                return true;
            else
                return false;
        }

        public static List<Categoria> ListaCategorias()
        {
            return CONN.Table<Categoria>().ToList();
        }

        #endregion


        #region Posts Favoritos

        public static void SalvaPostFavorito(Post P)
        {
            if (!PostEhFavorito(P))
            {
                P.author.PostID = P.id;
                CONN.Insert(P.author);

                foreach (var C in P.categories)
                {
                    C.PostID = P.id;
                    CONN.Insert(C);
                }

                foreach (var C in P.comments)
                {
                    C.PostID = P.id;
                    CONN.Insert(C);
                }

                foreach (var T in P.tags)
                {
                    T.PostID = P.id;
                    CONN.Insert(T);
                }

                CONN.Insert(P);
            }
                
        }

        public static void RemovePostFavorito(Post P)
        {
            if (PostEhFavorito(P))
                CONN.Delete(P, recursive: true);
        }

        public static bool PostEhFavorito(Post P)
        {
            int quantidade = CONN.ExecuteScalar<int>("Select count(*) from Post where id=?", P.id);

            if (quantidade > 0)
                return true;
            else
                return false;
        }

        public static List<Post> ListaPostsFavoritos()
        {
            return CONN.GetAllWithChildren<Post>().ToList();
        }

        #endregion

    }
}
