using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas
{
    public class DB
    {
        public DB()
        {
            DADOS = new ObservableCollection<DADO>();
        }

        public class DADO
        {
            public string ID { get; set; }
            public string Nome { get; set; }
        }

        public ObservableCollection<DADO> DADOS
        { get; set; }

        public void AdicionaItens()
        {
            DADOS.Add(new DADO() { ID = Guid.NewGuid().ToString(), Nome = Guid.NewGuid().ToString() });
            DADOS.Add(new DADO() { ID = Guid.NewGuid().ToString(), Nome = Guid.NewGuid().ToString() });
            DADOS.Add(new DADO() { ID = Guid.NewGuid().ToString(), Nome = Guid.NewGuid().ToString() });
            DADOS.Add(new DADO() { ID = Guid.NewGuid().ToString(), Nome = Guid.NewGuid().ToString() });
            DADOS.Add(new DADO() { ID = Guid.NewGuid().ToString(), Nome = Guid.NewGuid().ToString() });
        }

    }
}
