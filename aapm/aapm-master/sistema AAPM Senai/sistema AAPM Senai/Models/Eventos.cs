using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace sistema_AAPM_Senai.Models
{
    public class Eventos
    {
        //Conexão com o banco
        static SqlConnection con = new SqlConnection("Server=ESN509VMSSQL;DataBase=TCCjpg;User id=aluno;Password=Senai1234");
        int id_evento, cod_escola;
        string final, nome_evento, comeco, descricao;

        public int Id_evento
        {
            get
            {
                return id_evento;
            }

            set
            {
                id_evento = value;
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

        public string Final
        {
            get
            {
                return final;
            }

            set
            {
                final = value;
            }
        }

        public string Nome_evento
        {
            get
            {
                return nome_evento;
            }

            set
            {
                nome_evento = value;
            }
        }

        public string Comeco
        {
            get
            {
                return comeco;
            }

            set
            {
                comeco = value;
            }
        }

        public string Descricao
        {
            get
            {
                return descricao;
            }

            set
            {
                descricao = value;
            }
        }

        /******************* cadastro de Eventos *********************/
        internal string CadastroEventos()
        {
            try
            {
                con.Close();
                con.Open();
                SqlCommand query =
                    new SqlCommand("INSERT INTO Eventos VALUES(@id_evento, @final, @nome_evento, @comeco, @descricao, @cod_escola)", con);
                query.Parameters.AddWithValue("@id_evento", Id_evento);
                query.Parameters.AddWithValue("@final", final);
                query.Parameters.AddWithValue("@nome_evento", Nome_evento);
                query.Parameters.AddWithValue("@comeco", Comeco);
                query.Parameters.AddWithValue("@descricao", Descricao);
                query.Parameters.AddWithValue("@cod_escola", Cod_escola);
                
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

        /******************* listar eventos *********************/
        public static List<Eventos> ListaEventos()
        {
            List<Eventos> lista = new List<Eventos>();

            try

            {
                //abrindo conexão
                con.Open();

                SqlCommand query = new SqlCommand("SELECT * FROM Eventos", con);
                SqlDataReader leitor = query.ExecuteReader();
                while (leitor.Read())
                {

                    Eventos u = new Eventos();
                    
                    u.Id_evento = (int)leitor["id_evento"];
                    u.Final = leitor["final"].ToString();
                    u.Nome_evento = leitor["nome_evento"].ToString();
                    u.Comeco = leitor["comeco"].ToString();
                    u.Descricao = leitor["descricao"].ToString();
                    u.Cod_escola = (int)leitor["cod_escola"];

                    lista.Add(u);
                }
            }
            catch (Exception e)
            {
                lista = new List<Eventos>();
            }

            if (con.State == ConnectionState.Open)

                //fechando conexão
                con.Close();

            return lista;
        }

        /******************* Alterar dados dos eventos *********************/
        internal string Editar()
        {
            string res = "Salvo com sucesso!";

            try
            {
                //abrindo conexão
                con.Open();
                SqlCommand query =
                     new SqlCommand("UPDATE Eventos SET " +
                     "Id_evento = @id_evento,  " +
                     "Final = @final, Nome_evento = @nome_evento, Comeco = comeco, Descricao = @descricao, Cod_escola = @cod_escola  Where Id_evento = @id_evento", con);
                query.Parameters.AddWithValue("@id_evento", Id_evento);
                query.Parameters.AddWithValue("@final", final);
                query.Parameters.AddWithValue("@nome_evento", nome_evento);
                query.Parameters.AddWithValue("@comeco", comeco);
                query.Parameters.AddWithValue("@descricao", descricao);
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

        /******************* Buscar Eventos *********************/
        public static Eventos BuscaEventos(int id)
        {
            Eventos li = new Eventos();

            try
            {

                // abrindo conexão
                con.Open();


                SqlCommand query = new SqlCommand("SELECT * FROM Eventos WHERE Id_evento = @id_evento", con);
                query.Parameters.AddWithValue("@id_evento", id);
                SqlDataReader leitor = query.ExecuteReader();

                while (leitor.Read())
                {
                    li.id_evento = (int)leitor["id_evento"];
                    li.final = leitor["final"].ToString();
                    li.nome_evento = leitor["nome_evento"].ToString();
                    li.comeco = leitor["comeco"].ToString();
                    li.descricao = leitor["descricao"].ToString();
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

        /******************* Excluir Eventos *********************/
        internal string Remover()
        {
            string res = "Removido com sucesso!";

            try
            {

                //abrindo conexão
                con.Open();

                SqlCommand query =
                   new SqlCommand("DELETE FROM Eventos WHERE Id_evento = @id_evento", con);
                query.Parameters.AddWithValue("@id_evento", Id_evento);

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