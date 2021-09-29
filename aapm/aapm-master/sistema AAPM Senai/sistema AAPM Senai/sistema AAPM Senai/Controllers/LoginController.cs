using sistema_AAPM_Senai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistema_AAPM_Senai.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(int usuario, string senha)
        {
            Login log = null;

            log = Login.ValidarLogin(usuario, senha);



            if (log != null)
            {

                Session["User"] = log;
                TempData["Msg"] = "Logado com sucesso";

                return RedirectToAction("Principal", "Principal");


            }

            else
            {

                TempData["Msg"] = "Erro ao Logar";

            }

            return RedirectToAction("Index");

        }

        public ActionResult Sair()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}