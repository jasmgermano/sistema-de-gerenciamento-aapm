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
        String preco, nome_produto, img;
        Byte[] imagem;
        string imgstr;

        public int Cod_produto
        {
            get
            {
                return cod_produto;
            }

            set
            {
                cod_produto = value;
            }
        }

        public int Qtd
        {
            get
            {
                return qtd;
            }

            set
            {
                qtd = value;
            }
        }

        public int Cod_escola
        {
            get
            {
                return cod_escola;
            }

            set
            {
                cod_escola = value;
            }
        }

        public string Preco
        {
            get
            {
                return preco;
            }

            set
            {
                preco = value;
            }
        }

        public string Nome_produto
        {
            get
            {
                return nome_produto;
            }

            set
            {
                nome_produto = value;
            }
        }

        public Byte[] Imagem
        {

            get
            {
                return imagem;
            }

            set
            {
                imagem = value;
            }
        }

        public string Img
        {
            get
            {
                return img;
            }

            set
            {
                img = value;
            }
        }

        public string Imgstr
        {
            get
            {
                return imgstr;
            }

            set
            {
                imgstr = value;
            }
        }

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

            Produtos p = new Produtos();

            try
            {
                //abrindo conexão
                con.Open();
                //int cod_produto, qtd, cod_escola;
                //String preco, nome_produto, img;
                //Byte[] imagem;
                SqlCommand query = new SqlCommand("SELECT a.cod_produto, a.preco, a.nome_produto, a.qtd, a.cod_escola, b.imagem FROM Produtos as A JOIN Img as B on  A.cod_produto = B.cod_produto", con);
                SqlDataReader leitor = query.ExecuteReader();
                while (leitor.Read())
                {

                    Produtos u = new Produtos();
                    u.Cod_produto = (int)leitor["cod_produto"];
                    u.Preco = leitor["preco"].ToString();
                    u.Nome_produto = leitor["nome_produto"].ToString();
                    u.Qtd = (int)leitor["qtd"];
                    u.Cod_escola = (int)leitor["cod_escola"];
                    u.Imagem = (byte[])leitor["imagem"];
                    u.Imgstr = Convert.ToBase64String(u.imagem);
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
                query.Parameters.AddWithValue("@cod_produto", Cod_produto);
                query.Parameters.AddWithValue("@preco", Preco);
                query.Parameters.AddWithValue("@nome_produto", Nome_produto);
                query.Parameters.AddWithValue("@qtd", Qtd);
                query.Parameters.AddWithValue("@cod_escola", Cod_escola);

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

                    li.Cod_produto = (int)leitor["cod_produto"];
                    li.Preco = leitor["preco"].ToString();
                    li.Nome_produto = leitor["nome_produto"].ToString();
                    li.Qtd = (int)leitor["qtd"];
                    li.Cod_escola = (int)leitor["cod_escola"];

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
                   new SqlCommand("DELETE FROM Img WHERE Cod_produto = @cod_produto", con);
                query.Parameters.AddWithValue("@cod_produto", Cod_produto);
                SqlCommand query2 =
                   new SqlCommand("DELETE FROM Produtos WHERE Cod_produto = @cod_produto", con);
                query2.Parameters.AddWithValue("@cod_produto", Cod_produto);

                query.ExecuteNonQuery();
                query2.ExecuteNonQuery();
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

        public static Produtos detalhesProdutos(string det)
        {

            List<string> listaImg = new List<string>();

            Produtos p = new Produtos();

            try
            {
                //abrindo conexão
                con.Open();
                //int cod_produto, qtd, cod_escola;
                //String preco, nome_produto, img;
                //Byte[] imagem;
                SqlCommand query = new SqlCommand("SELECT a.cod_produto, a.preco, a.nome_produto, a.qtd, a.cod_escola, b.imagem FROM Produtos as A JOIN Img as B on  A.cod_produto = B.cod_produto WHERE A.Cod_produto = @cod_produto", con);

                query.Parameters.AddWithValue("@cod_produto", det);
                SqlDataReader leitor = query.ExecuteReader();
                while (leitor.Read())
                {

                    p.cod_produto = (int)leitor["cod_produto"];
                    p.preco = leitor["preco"].ToString();
                    p.nome_produto = leitor["nome_produto"].ToString();
                    p.qtd = (int)leitor["qtd"];
                    p.cod_escola = (int)leitor["cod_escola"];
                    p.Imagem = (byte[])leitor["imagem"];
                    //li.Img = Convert.ToBase64String(li.Imagem);
                }
            }

            catch (Exception e)
            {
                p = null;
            }

            if (con.State == ConnectionState.Open)

                //fechando conexão
                con.Close();

            return p;

        }
    }
}