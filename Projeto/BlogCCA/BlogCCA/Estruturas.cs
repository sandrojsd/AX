using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using System.IO;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace BlogCCA
{

    #region Categorias

    public class Categoria
    {
        [PrimaryKey]
        public int id { get; set; }

        public string slug { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int parent { get; set; }
        public int post_count { get; set; }

        [Ignore]
        public string quantidade { get { return "(" + post_count.ToString() + ")"; } }
    }

    public class RETORNO_CATEGORIA
    {
        public string status { get; set; }
        public int count { get; set; }
        public List<Categoria> categories { get; set; }
    }

    #endregion


    #region Post


    public class Post
    {
        [PrimaryKey]
        public int id { get; set; }

        public string type { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public string title_plain { get; set; }
        public string content { get; set; }
        public string excerpt { get; set; }
        public string date { get; set; }
        [Ignore]
        public string Data { get {
                DateTime DT = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", null);
                return DT.ToString("dd/MM/yyyy");
            } }
        public string modified { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Tag> tags { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<CategoriaPost> categories { get; set; }


        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Comentario> comments { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Author author { get; set; }

        public int comment_count { get; set; }
        public string comment_status { get; set; }
        public string thumbnail { get; set; }
        public string thumbnail_size { get; set; }
    }

    public class RETORNO_POSTS
    {
        public string status { get; set; }
        public int count { get; set; }
        public int count_total { get; set; }
        public int pages { get; set; }
        public List<Post> posts { get; set; }
    }



    #endregion


    #region Diversos

    public class CategoriaPost
    {
        [PrimaryKey, AutoIncrement]
        public int AutoID { get; set; }


        public int id { get; set; }

        public string slug { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int parent { get; set; }
        public int post_count { get; set; }

        [ForeignKey(typeof(Post))]
        public int PostID { get; set; }
        [ManyToOne]
        public Post POST { get; set; }
    }

    public class Tag
    {
        [PrimaryKey, AutoIncrement]
        public int AutoID { get; set; }

        public int id { get; set; }
        public string slug { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int post_count { get; set; }

        [ForeignKey(typeof(Post))]
        public int PostID { get; set; }
        [ManyToOne]
        public Post POST { get; set; }
    }

    public class Comentario
    {
        [PrimaryKey]
        public int id { get; set; }

        public string name { get; set; }
        public string url { get; set; }
        public string date { get; set; }
        public string content { get; set; }


        [ForeignKey(typeof(Post))]
        public int PostID { get; set; }
        [ManyToOne]
        public Post POST { get; set; }


        public int parent { get; set; }

    }

    public class Author
    {
        [PrimaryKey, AutoIncrement]
        public int AutoID { get; set; }

        public int id { get; set; }

        public string slug { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string nickname { get; set; }
        public string url { get; set; }
        public string description { get; set; }

        [ForeignKey(typeof(Post))]
        public int PostID { get; set; }
        [OneToOne]
        public Post POST { get; set; }

    }

    #endregion

}
