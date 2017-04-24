using InstaXamarinMobile.Custom;
using InstaXamarinMobile.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InstaXamarinMobile.Util
{
    public class DBHelper : IDisposable
    {
        SQLiteAsyncConnection database;

        public DBHelper()
        {
            database = new SQLiteAsyncConnection(DependencyService.Get<IArquivo>().LocalBanco("InstaXamarin.db3"));
            database.CreateTableAsync<Post>().Wait();
        }

        //Salva Cache
        public void SalvarTudo(List<Post> Posts)
        {
            //Limpa Tabela
            database.ExecuteAsync("delete from post").Wait();

            foreach (var P in Posts)
                P.SerializaUsuarioDados();

            //Adiciona todos
            database.InsertAllAsync(Posts);
        }

        //Get Cache
        public async Task<List<Post>> GetCache()
        {
            List<Post> Posts = await database.Table<Post>().ToListAsync();

            foreach (var P in Posts)
                P.DesSerializaUsuarioDados();

            return Posts.OrderByDescending(pp=>pp.Data).ToList();
        }

        public void Dispose()
        {
        }
    }
}
