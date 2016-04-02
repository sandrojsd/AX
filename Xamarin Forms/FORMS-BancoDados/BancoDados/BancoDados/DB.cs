using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BancoDados
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }

    [Table("Teste")]
    public class TesteDado
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nome { get; set; }
    }

    public class DB
    {
        SQLiteConnection database;

        public DB()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<TesteDado>();
        }

        public void AdicionaTesteDado()
        {
            //database.Insert(new TesteDado() { Nome = Guid.NewGuid().ToString() });
        }
        public IEnumerable<TesteDado> GetTesteDados()
        {
            return database.Table<TesteDado>().ToList();
        }
        public TesteDado GetTesteDado(int id)
        {
            return database.Table<TesteDado>().FirstOrDefault(x => x.Id == id);
        }
        public int DeleteTestDado(int id)
        {
            return database.Delete<TesteDado>(id);
        }
    }
}
