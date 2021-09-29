using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace sistema_AAPM_Senai.Models
{
    public class Login
    {
        //conexão com o banco de dados
        static SqlConnection con =
         new SqlConnection("Server=ESN509VMSSQL;Database=TCCjpg;User id = aluno;Password=Senai1234");
        private int usuario, cargo;
        private String senha;

        public int Usuario
        {
            get
            {
                return usuario;
            }

            set
            {
                usuario = value;
            }
        }

        public string Senha
        {
            get
            {
                return senha;
            }

            set
            {
                senha = value;
            }
        }
        public int Cargo
        {
            get
            {
                return cargo;
            }

            set
            {
                cargo = value;
            }
        }
        public static Login ValidarLogin(int usuario, string senha)
        {
            Login log = null;

            try
            {
                con.Open();
                // mostrando os campos para selecionar os dados do banco
                SqlCommand query = new SqlCommand("SELECT*FROM Usuarios WHERE Cod_Usuario=@cod_usuario and senha=@senha", con);
                query.Parameters.AddWithValue("cod_usuario", usuario);
                query.Parameters.AddWithValue("senha", senha);

                SqlDataReader leitor = query.ExecuteReader();

                if (leitor.Read())
                {
                    log = new Login();
                    log.Usuario = usuario;
                    log.Senha = senha;
                    log.Cargo = (int)leitor["cargo"];
                }

            }

            catch (Exception e)
            {

                log = null;
            }

            if (con.State == ConnectionState.Open)
                con.Close();


            return log;
        }
    }
}