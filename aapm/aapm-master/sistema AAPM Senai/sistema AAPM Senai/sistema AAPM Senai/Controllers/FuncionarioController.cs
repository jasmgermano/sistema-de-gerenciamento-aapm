using sistema_AAPM_Senai.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistema_AAPM_Senai.Controllers
{
    public class FuncionarioController : Controller
    {
        static SqlConnection con = new SqlConnection("Server=ESN509VMSSQL;DataBase=TCCjpg;User id=aluno;Password=Senai1234");
        // GET: Funcionario
        public ActionResult Funcionario()
        {
            return View();
        }
        public ActionResult listaAlunos()
        {
            return View(Usuario.ListaAlunos());
        }
        /******************* cadastro de Alunos *********************/
        public ActionResult CadastroAlunos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastroAlunos(int cod_usuario, string email, string tipo_usuario, string senha, string rua, string cidade, string bairro, string nome, int cod_escola, int numero, int ddd)
        {
            Usuario u = new Usuario();
            u.Cod_usuario = cod_usuario;
            u.Email = email;
            u.Tipo_usuario = "Aluno";
            u.Senha = senha;
            u.Rua = rua;
            u.Cidade = cidade;
            u.Bairro = bairro;
            u.Nome = nome;
            u.Cod_escola = cod_escola;
            u.Cargo = 2;
            u.Numero = numero;
            u.Ddd = ddd;

            TempData["Msg"] = u.CadastroFuncionarios();
            return RedirectToAction("cadastroAlunos");
        }

        /******************* edição de Alunos *********************/
        public ActionResult editarAlunos(int id)
        {
            Usuario u = Usuario.BuscaUsuario(id);

            if (u == null)
            {
                TempData["Msg"] = "Erro ao buscar produto!";

                return RedirectToAction("listaAlunos");
            }
            return View(u);
        }

        [HttpPost]
        public ActionResult editarAlunos(int cod_usuario, string email, string tipo_usuario,
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

                return RedirectToAction("listaAlunos");
            else
                return View();
        }

        /******************* exclusão de Alunos *********************/
        public ActionResult deletarAlunos(int id)
        {
            Usuario u = new Usuario();

            u.Cod_usuario = id;
            TempData["Msg"] = u.Remover();

            return RedirectToAction("listaAlunos");
        }

        /******************* detalhes de Alunos *********************/
        public ActionResult detalhesAlunos(string id)
        {
            Usuario co = Usuario.detalhes(id);

            if (co == null)
            {
                TempData["Msg"] = "Erro ao buscar produto!";

                return RedirectToAction("listaAlunos");
            }
            return View(co);
        }

        /******************* Cadastro de Proodutos *********************/
        public ActionResult cadastroProdutos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult cadastroProdutos(int cod_produto, string preco, string nome_produto, int qtd, int cod_escola)
        {
            foreach (string nomeArquivo in Request.Files)
            {
                HttpPostedFileBase arqPostado = Request.Files[nomeArquivo];
                int tamConteudo = arqPostado.ContentLength; //pega tamanho
                string tipoArq = arqPostado.ContentType; //pega o tipo

                //converter para bytes
                byte[] imgBytes = new byte[tamConteudo];
                arqPostado.InputStream.Read(imgBytes, 0, tamConteudo);

                con.Open();
                SqlCommand qry = new SqlCommand
                    ("INSERT INTO Img(Imagem) VALUES(@imagem)", con);
                qry.Parameters.AddWithValue("@imagem", imgBytes);

                //qry.ExecuteNonQuery();
                con.Close();


                Produtos p = new Produtos();


                p.Cod_produto = cod_produto;
                p.Preco = preco;
                p.Nome_produto = nome_produto;
                p.Qtd = qtd;
                p.Cod_escola = cod_escola;
                p.Imagem = imgBytes;


                TempData["Msg"] = p.cadastroProdutos();


                return RedirectToAction("listaProdutos");

            }

            return RedirectToAction("listaProdutos");
        }
        /******************* edição de Produtos *********************/
        public ActionResult editarProdutos(int id)
        {
            Produtos u = Produtos.BuscaProdutos(id);

            if (u == null)
            {
                TempData["Msg"] = "Erro ao buscar produto!";

                return RedirectToAction("listaAlunos");
            }
            return View(u);
        }

        [HttpPost]
        public ActionResult editarProdutos(int cod_produto, string preco, string nome_produto, int qtd, int cod_escola)
        {
            Produtos p = new Produtos();


            p.Cod_produto = cod_produto;
            p.Preco = preco;
            p.Nome_produto = nome_produto;
            p.Qtd = qtd;
            p.Cod_escola = cod_escola;

            string res = p.Editar();

            TempData["Msg"] = res;

            if (res == "Salvo com sucesso!")

                return RedirectToAction("listaProdutos");
            else
                return View();
        }

        /******************* exclusão de Produtos *********************/
        public ActionResult deletarProdutos(int id)
        {
            Produtos u = new Produtos();

            u.Cod_produto = id;
            TempData["Msg"] = u.Remover();

            return RedirectToAction("listaProdutos");
        }

    }
}