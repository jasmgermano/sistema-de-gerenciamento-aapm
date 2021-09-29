using sistema_AAPM_Senai.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistema_AAPM_Senai.Controllers
{
    public class EventosController : Controller
    {
        static SqlConnection con = new SqlConnection("Server=ESN509VMSSQL;DataBase=TCCjpg;User id=aluno;Password=Senai1234");
        // GET: Eventos
        public ActionResult Evt()
        {
            return View();
        }
        /******************* listar eventos *********************/
        public ActionResult listaEventos()
        {
            return View(Eventos.ListaEventos());
        }  
        /******************* cadastro de funcionários *********************/
        public ActionResult CadastroEventos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastroEventos(int id_evento, string final, string nome_evento, string comeco, string descricao, int cod_escola)
        {
            Eventos u = new Eventos();
            u.Id_evento = id_evento;
            u.Final = final;
            u.Nome_evento = nome_evento;
            u.Comeco = comeco;
            u.Descricao = descricao;
            u.Cod_escola = cod_escola;
            TempData["Msg"] = u.CadastroEventos();
            return RedirectToAction("cadastroEventos");
        }

        /******************* edição de Eventos *********************/
        public ActionResult editarEventos(int id)
        {
            Eventos u = Eventos.BuscaEventos(id);

            if (u == null)
            {
                TempData["Msg"] = "Erro ao buscar produto!";

                return RedirectToAction("listaEventos");
            }
            return View(u);
        }

        [HttpPost]
        public ActionResult editarEventos(int id_evento, string final, string nome_evento, string comeco, string descricao, int cod_escola)
        {
            Eventos u = new Eventos();
            u.Id_evento = id_evento;
            u.Final = final;
            u.Nome_evento = nome_evento;
            u.Comeco = descricao;
            u.Cod_escola = cod_escola;

            string res = u.Editar();

            TempData["Msg"] = res;

            if (res == "Salvo com sucesso!")

                return RedirectToAction("listaEventos");
            else
                return View();
        }

        /******************* exclusão de funcionários *********************/
        public ActionResult deletarEventos(int id)
        {
            Eventos u = new Eventos();

            u.Id_evento = id;
            TempData["Msg"] = u.Remover();

            return RedirectToAction("listaEventos");
        }
    }
}