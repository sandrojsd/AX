using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCCA
{
    public interface iRecursosNativos
    {
        String GetCaminho(String Arquivo);

        SQLite.Net.Interop.ISQLitePlatform SQLitePlataform { get; }
    }
}
