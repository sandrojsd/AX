using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWS_Teste
{
    public partial class EstruturaDados
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
    }


    public interface iWS
    {
        List<EstruturaDados> Listar();
        EstruturaDados Gravar(EstruturaDados Dados);
    }

    public class WS
    {

    }
}
