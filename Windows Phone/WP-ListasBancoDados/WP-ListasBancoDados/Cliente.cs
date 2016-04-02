using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace WP_ListasBancoDados
{
    public class Cliente
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public String Nome { get; set; }
    }
}
