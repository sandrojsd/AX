using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


[assembly: Xamarin.Forms.Dependency(typeof(AppWS_Teste.iOS.FormsWS))]
namespace AppWS_Teste.iOS
{
    public class FormsWS : iWS
    {
        public EstruturaDados Gravar(EstruturaDados Dados)
        {
            WS.WS WEBSERVICE = new iOS.WS.WS();
            WEBSERVICE.Url = "http://10.0.1.5:3000/WS.asmx";
            return Cast<EstruturaDados>(WEBSERVICE.Gravar(Cast<WS.EstruturaDados>(Dados)));
        }

        public List<EstruturaDados> Listar()
        {
            WS.WS WEBSERVICE = new iOS.WS.WS();
            WEBSERVICE.Url = "http://10.0.1.5:3000/WS.asmx";

            List<EstruturaDados> RET = new List<EstruturaDados>();

            foreach (var Dado in WEBSERVICE.Listar())
                RET.Add(Cast<EstruturaDados>(Dado));

            return RET;
        }

        T Cast<T>(object Data)
        {
            T RET = (T)Activator.CreateInstance(typeof(T));

            foreach (var P in RET.GetType().GetProperties())
                P.SetValue(RET, Data.GetType().GetProperty(P.Name).GetValue(Data));

            return RET;
        }
    }
}