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
        static SqlConnection con = new SqlConnection("Server=ESN509VMSSQL;DataBase=TCCjpg;User id=aluno;Password=Senai1234");
        String final, comeco;
        String nome_evento, descricao;
        int cod_escola, id_evento;

        public String Final
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

        public String Comeco
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
                    u.id_evento = (int)leitor["id_evento"];
                    u.cod_escola = (int)leitor["cod_escola"];
                    u.Comeco = leitor["comeco"].ToString();
                    u.final = leitor["final"].ToString();
                    u.nome_evento= leitor["nome_evento"].ToString();
                    u.descricao = leitor["descricao"].ToString();

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
    }
    }