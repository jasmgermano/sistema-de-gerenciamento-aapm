using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace sistema_AAPM_Senai.Models
{
    public class Produtos
    {
        static SqlConnection con = new SqlConnection("Server=ESN509VMSSQL;DataBase=TCCjpg;User id=aluno;Password=Senai1234");
        int cod_produto, qtd, cod_escola;
        String preco, nome_produto;
        Byte[] imagem;

        public int Cod_produto { get => cod_produto; set => cod_produto = value; }
        public int Qtd { get => qtd; set => qtd = value; }
        public int Cod_escola { get => cod_escola; set => cod_escola = value; }
        public string Preco { get => preco; set => preco = value; }
        public string Nome_produto { get => nome_produto; set => nome_produto = value; }
        public byte[] Imagem { get => imagem; set => imagem = value; }

        public string cadastroProdutos()
        {
            string res = "Inserido com sucesso!";

            try
            {
                //abrindo conexão
                con.Open();

                SqlCommand query =
                   new SqlCommand("INSERT INTO Produtos VALUES(@cod_produto, @preco, @nome_produto, @qtd, @cod_escola)", con);
                query.Parameters.AddWithValue("@cod_produto", cod_produto);
                query.Parameters.AddWithValue("@preco", preco);
                query.Parameters.AddWithValue("@nome_produto", nome_produto);
                query.Parameters.AddWithValue("@qtd", qtd);
                query.Parameters.AddWithValue("@cod_escola", cod_escola);

                SqlCommand query2 =
                   new SqlCommand("INSERT INTO Img VALUES(@imagem, @cod_produto)", con);
                query2.Parameters.AddWithValue("@imagem", imagem);
                query2.Parameters.AddWithValue("@cod_produto", cod_produto);

                query.ExecuteNonQuery();
                query2.ExecuteNonQuery();


            }
            catch (Exception e)
            {
                return e.Message;
            }

            if (con.State == ConnectionState.Open)

                //fechando conexão
                con.Close();

            return "Inserido com sucesso!";
        }
        public static List<Produtos> ListaProdutos()
        {
            List<Produtos> lista = new List<Produtos>();

            try

            {
                //abrindo conexão
                con.Open();

                SqlCommand query = new SqlCommand("SELECT * FROM Produtos", con);
                SqlDataReader leitor = query.ExecuteReader();
                while (leitor.Read())
                {

                    Produtos u = new Produtos();
                    u.cod_produto = (int)leitor["cod_produto"];
                    u.preco = leitor["preco"].ToString();
                    u.nome_produto = leitor["bairro"].ToString();
                    u.qtd = (int)leitor["qtd"];
                    u.cod_escola = (int)leitor["cod_escola"];

                    lista.Add(u);
                }
            }
            catch (Exception e)
            {
                lista = new List<Produtos>();
            }

            if (con.State == ConnectionState.Open)

                //fechando conexão
                con.Close();

            return lista;
        }

        /******************* Alterar dados dos produtos *********************/
        internal string Editar()
        {
            string res = "Salvo com sucesso!";

            try
            {
                //abrindo conexão
                con.Open();
                SqlCommand query =
                     new SqlCommand("UPDATE Produtos SET " +
                     "Cod_produto = @cod_produto,  " +
                     "Preco = @preco, Nome_produto = @nome_produto, Qtd = qtd, Cod_escola = @cod_escola Where Cod_produto = @cod_produto", con);
                query.Parameters.AddWithValue("@cod_produto", cod_produto);
                query.Parameters.AddWithValue("@preco", preco);
                query.Parameters.AddWithValue("@nome_produto", nome_produto);
                query.Parameters.AddWithValue("@qtd", qtd);
                query.Parameters.AddWithValue("@cod_escola", cod_escola);

                query.ExecuteNonQuery();
            }

            catch (Exception e)
            {
                res = e.Message;
            }

            if (con.State == System.Data.ConnectionState.Open)

                //fechando conexão
                con.Close();

            return res;
        }

        /******************* Buscar AAPM's *********************/
        public static Produtos BuscaProdutos(int cod_produto)
        {
            Produtos li = new Produtos();

            try
            {

                // abrindo conexão
                con.Open();


                SqlCommand query = new SqlCommand("SELECT * FROM Produtos WHERE Cod_produto = @cod_produto", con);
                query.Parameters.AddWithValue("@cod_produto", cod_produto);
                SqlDataReader leitor = query.ExecuteReader();

                while (leitor.Read())
                {

                    li.cod_produto = (int)leitor["cod_produto"];
                    li.preco = leitor["preco"].ToString();
                    li.nome_produto = leitor["nome_produto"].ToString();
                    li.qtd = (int)leitor["qtd"];
                    li.cod_escola = (int)leitor["cod_escola"];

                }
            }

            catch (Exception e)
            {
                li = null;
            }

            if (con.State == ConnectionState.Open)

                //fechando conexão
                con.Close();

            return li;
        }

        /******************* Excluir AAPM's *********************/
        internal string Remover()
        {
            string res = "Removido com sucesso!";

            try
            {

                //abrindo conexão
                con.Open();

                SqlCommand query =
                   new SqlCommand("DELETE FROM Produtos WHERE Cod_produto = @cod_produto", con);
                query.Parameters.AddWithValue("@cod_produto", cod_produto);

                query.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                res = e.Message;
            }

            if (con.State == ConnectionState.Open)

                //fechando conexão
                con.Close();

            return res;

        }
    }
}