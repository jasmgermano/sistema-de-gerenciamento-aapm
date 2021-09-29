using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace sistema_AAPM_Senai.Models
{
    public class AAPM
    {
        //Conexão com o banco
        static SqlConnection con = new SqlConnection("Server=ESN509VMSSQL;DataBase=TCCjpg;User id=aluno;Password=Senai1234");
        int cod_escola, numero;
        string nome_escola, bairro, rua, cidade;

        //Atributos
        public string Bairro
        {
            get
            {
                return bairro;
            }

            set
            {
                bairro = value;
            }
        }

        public string Cidade
        {
            get
            {
                return cidade;
            }

            set
            {
                cidade = value;
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

        public string Nome_escola
        {
            get
            {
                return nome_escola;
            }

            set
            {
                nome_escola = value;
            }
        }

        public int Numero
        {
            get
            {
                return numero;
            }

            set
            {
                numero = value;
            }
        }

        public string Rua
        {
            get
            {
                return rua;
            }

            set
            {
                rua = value;
            }
        }

        /******************* cadastro de AAPM *********************/
        internal string CadastroAAPM()
        {
            try
            {
                con.Close();
                con.Open();
                SqlCommand query =
                    new SqlCommand("INSERT INTO AAPM VALUES(@cod_escola, @nome_escola, @bairro, @rua, @numero, @cidade)", con);
                query.Parameters.AddWithValue("@cod_escola", cod_escola);
                query.Parameters.AddWithValue("@nome_escola", nome_escola);
                query.Parameters.AddWithValue("@bairro", bairro);
                query.Parameters.AddWithValue("@rua", rua);
                query.Parameters.AddWithValue("@numero", numero);
                query.Parameters.AddWithValue("@cidade", cidade);

                query.ExecuteNonQuery();
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

        /******************* listar AAPM's *********************/
        public static List<AAPM> ListaAAPM()
        {
            List<AAPM> listaEscola = new List<AAPM>();

            try

            {
                //abrindo conexão
                con.Open();

                SqlCommand query = new SqlCommand("SELECT * FROM AAPM", con);
                SqlDataReader leitor = query.ExecuteReader();
                while (leitor.Read())
                {

                    AAPM u = new AAPM();
                    u.cod_escola = (int)leitor["cod_escola"];
                    u.numero = (int)leitor["numero"];
                    u.nome_escola = leitor["nome_escola"].ToString();
                    u.bairro = leitor["bairro"].ToString();
                    u.rua = leitor["rua"].ToString();
                    u.cidade = leitor["cidade"].ToString();

                    listaEscola.Add(u);
                }
            }
            catch (Exception e)
            {
                listaEscola = new List<AAPM>();
            }

            if (con.State == ConnectionState.Open)

                //fechando conexão
                con.Close();

            return listaEscola;
        }

        /******************* Alterar dados de AAPM's *********************/
        internal string Editar()
        {
            string res = "Salvo com sucesso!";

            try
            {
                //abrindo conexão
                con.Open();
                //int cod_escola, numero;
                //string nome_escola, bairro, rua, cidade;
                SqlCommand query =
                     new SqlCommand("UPDATE AAPM SET " +
                     "Cod_escola = @cod_escola,  " +
                     "Nome_escola = @nome_escola, Bairro = @bairro, Rua = rua, Numero = @numero, Cidade = @cidade  Where Cod_escola = @cod_escola", con);
                query.Parameters.AddWithValue("@cod_escola", cod_escola);
                query.Parameters.AddWithValue("@nome_escola", nome_escola);
                query.Parameters.AddWithValue("@bairro", bairro);
                query.Parameters.AddWithValue("@rua", rua);
                query.Parameters.AddWithValue("@numero", numero);
                query.Parameters.AddWithValue("@cidade", cidade);

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
        public static AAPM BuscaAAPM(int cod_escola)
        {
            AAPM li = new AAPM();

            try
            {

                // abrindo conexão
                con.Open();


                SqlCommand query = new SqlCommand("SELECT * FROM AAPM WHERE Cod_escola = @cod_escola", con);
                query.Parameters.AddWithValue("@cod_escola", cod_escola);
                SqlDataReader leitor = query.ExecuteReader();

                while (leitor.Read())
                {

                    li.cod_escola = (int)leitor["cod_escola"];
                    li.numero = (int)leitor["numero"];
                    li.nome_escola = leitor["nome_escola"].ToString();
                    li.bairro = leitor["bairro"].ToString();
                    li.rua = leitor["rua"].ToString();
                    li.cidade = leitor["cidade"].ToString();

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
                   new SqlCommand("DELETE FROM AAPM WHERE Cod_escola = @cod_escola", con);
                query.Parameters.AddWithValue("@cod_escola", cod_escola);

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
