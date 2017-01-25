using InstaXamarinWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;

namespace InstaXamarinWeb.Util
{
    public class APIAutorizacao : System.Web.Http.Filters.AuthorizationFilterAttribute
    {
        public override bool AllowMultiple
        {
            get { return false; }
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //Validação
            if (actionContext.ControllerContext.Request.Headers.Any(hh => hh.Key == "token"))
            {
                String TokenLogado = actionContext.ControllerContext.Request.Headers.First(hh => hh.Key == "token").Value.First();

                using (DB Banco = new DB())
                {
                    AccessToken AT = Banco.AccessTokens.Find(TokenLogado);

                    //Token existe
                    if (AT != null)
                        return;
                }
            }

            //Não autorizado
            actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

        }
    }
}