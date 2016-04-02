using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files
{
    public interface IFile
    {
        String Caminho { get; }

        Task Salvar(string filename, string text);
        string Ler(string filename);
    }
}
