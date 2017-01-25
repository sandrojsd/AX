using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InstaXamarinWeb.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Post(String Senha)
        {
            if (Senha == "teste")
            {
                Session["Logado"] = true;
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Sair()
        {
            Session["Logado"] = null;
            return RedirectToAction("Index");
        }
    }
}