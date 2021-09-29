using sistema_AAPM_Senai.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistema_AAPM_Senai.Controllers
{
    public class AdmController : Controller
    {
        static SqlConnection con = new SqlConnection("Server=ESN509VMSSQL;DataBase=TCCjpg;User id=aluno;Password=Senai1234");
        public ActionResult Adm()
        {
            return View();
        }
        /******************* listar funcionários *********************/
        public ActionResult listaFuncionarios()
        {
            return View(Usuario.ListaFuncionarios());
        }
        /******************* listar AAPM's *********************/
        public ActionResult listaAAPM()
        {
            return View(AAPM.ListaAAPM());
        }
        /******************* cadastro de funcionários *********************/
        public ActionResult CadastroFuncionarios()
        {
            List<AAPM> escola = new List<AAPM>();

            con.Open();

            SqlCommand qry = new SqlCommand
                ("SELECT * FROM AAPM", con);

            SqlDataReader leitor = qry.ExecuteReader();


            while (leitor.Read())
            {

                AAPM a = new AAPM();
                a.Cod_escola = (int)leitor["cod_escola"];

                escola.Add(a);
            }

            con.Close();

            ViewBag.escola = escola;

            return View();
        }
        
        [HttpPost]
        public ActionResult CadastroFuncionarios(int cod_usuario, string email, string tipo_usuario, string senha, string rua, string cidade, string bairro, string nome, int cod_escola, int numero, int ddd)
        {
            Usuario u = new Usuario();
            u.Cod_usuario = cod_usuario;
            u.Email = email;
            u.Tipo_usuario = "Funcionário";
            u.Senha = senha;
            u.Rua = rua;
            u.Cidade = cidade;
            u.Bairro = bairro;
            u.Nome = nome;
            u.Cod_escola = cod_escola;
            u.Cargo = 1;
            u.Numero = numero;
            u.Ddd = ddd;

            TempData["Msg"] = u.CadastroFuncionarios();
            return RedirectToAction("CadastroFuncionarios");
        }

        /******************* edição de funcionários *********************/
        public ActionResult editarFuncionarios(int id)
        {
            Usuario u = Usuario.BuscaUsuario(id);

            if (u == null)
            {
                TempData["Msg"] = "Erro ao buscar produto!";

                return RedirectToAction("listaFuncionarios");
            }
            return View(u);
        }

        [HttpPost]
        public ActionResult editarFuncionarios(int cod_usuario, string email, string tipo_usuario,
            string senha, string rua, string cidade, string bairro, string nome, int cod_escola, int cargo, int numero, int ddd)
        {
            Usuario u = new Usuario();
            u.Cod_usuario = cod_usuario;
            u.Email = email;
            u.Tipo_usuario = tipo_usuario;
            u.Senha = senha;
            u.Rua = rua;
            u.Cidade = cidade;
            u.Bairro = bairro;
            u.Nome = nome;
            u.Cod_escola = cod_escola;
            u.Cargo = cargo;
            u.Numero = numero;
            u.Ddd = ddd;

            string res = u.Editar();

            TempData["Msg"] = res;

            if (res == "Salvo com sucesso!")

                return RedirectToAction("listaFuncionarios");
            else
                return View();
        }

        /******************* exclusão de funcionários *********************/
        public ActionResult deletarFuncionarios(int id)
        {
            Usuario u = new Usuario();

            u.Cod_usuario = id;
            TempData["Msg"] = u.Remover();

            return RedirectToAction("listaFuncionarios");
        }

        /******************* detalhes de funcionários *********************/
        public ActionResult detalhesFuncionarios(string id)
        {
            Usuario co = Usuario.detalhes(id);

            if (co == null)
            {
                TempData["Msg"] = "Erro ao buscar produto!";

                return RedirectToAction("listaFuncionarios");
            }
            return View(co);
        }


        /******************* cadastro de AAPM *********************/
        public ActionResult cadastroAAPM()
        {
            return View();
        }

        [HttpPost]
        public ActionResult cadastroAAPM(int cod_escola, string nome_escola, string bairro, string rua,  int numero, string cidade)
        {
            AAPM a = new AAPM();
            a.Cod_escola = cod_escola;
            a.Nome_escola = nome_escola;
            a.Bairro = bairro;
            a.Rua = rua;
            a.Numero = numero;
            a.Cidade = cidade;

            TempData["Msg"] = a.CadastroAAPM();
            return RedirectToAction("cadastroAAPM");
        }
        /******************* edição de AAPM's *********************/
        public ActionResult editarAAPM(int id)
        {
            AAPM u = AAPM.BuscaAAPM(id);

            if (u == null)
            {
                TempData["Msg"] = "Erro ao buscar produto!";

                return RedirectToAction("listaAAPM");
            }
            return View(u);
        }

        [HttpPost]
        public ActionResult editarAAPM(int cod_escola, int numero, string nome_escola,
            string bairro, string rua, string cidade)
        {
            AAPM a = new AAPM();
            a.Cod_escola = cod_escola;
            a.Nome_escola = nome_escola;
            a.Bairro = bairro;
            a.Rua = rua;
            a.Numero = numero;
            a.Cidade = cidade;

            string res = a.Editar();

            TempData["Msg"] = res;

            if (res == "Salvo com sucesso!")

                return RedirectToAction("listaAAPM");
            else
                return View();
        }

        /******************* exclusão de AAPM's *********************/
        public ActionResult deletarAAPM(int id)
        {
            AAPM a = new AAPM();

            a.Cod_escola = id;
            TempData["Msg"] = a.Remover();

            return RedirectToAction("listaAAPM");
        }



    }
}