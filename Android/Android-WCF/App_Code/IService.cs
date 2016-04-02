using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

[ServiceContract]
public interface IService
{
	[OperationContract]
    string TesteDados(int value);

	[OperationContract]
    Informacao TesteInformacao(Informacao Info);
}

[DataContract]
public class Informacao
{
    [DataMember]
    public bool Ativo
    { get; set; }

	[DataMember]
	public string Nome
    { get; set; }

}
