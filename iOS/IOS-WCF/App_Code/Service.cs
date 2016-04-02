using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

public class Service : IService
{
	public string TesteDados(int valor)
	{
        return string.Format("Você informou: {0}", valor);
	}

    public Informacao TesteInformacao(Informacao Info)
    {
        return Info;
    }
}
