using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace sistema_AAPM_Senai.Models
{
    public class Usuario
    {
        //conexão com o banco
        static SqlConnection con = new SqlConnection("Server=ESN509VMSSQL;DataBase=TCCjpg;User id=aluno;Password=Senai1234");
        int cod_usuario, cod_escola, numero, ddd, cargo;
        string email, tipo_usuario, senha, rua, cidade, bairro, nome;

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

        public int Cod_usuario
        {
            get
            {
                return cod_usuario;
            }

            set
            {
                cod_usuario = value;
            }
        }

        public int Ddd
        {
            get
            {
                return ddd;
            }

            set
            {
                ddd = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public string Nome
        {
            get
            {
                return nome;
            }

            set
            {
                nome = value;
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

        public string Tipo_usuario
        {
            get
            {
                return tipo_usuario;
            }

            set
            {
                tipo_usuario = value;
            }
        }

        /******************* cadastro de Usuarios *********************/
        internal string CadastroFuncionarios()
        {
            try
            {
                con.Close();
                con.Open();
                SqlCommand query =
                    new SqlCommand("INSERT INTO Usuarios VALUES(@cod_usuario, @email, @tipo_usuario, @senha, @rua, @cidade, @bairro, @nome, @cod_escola, @cargo)", con);
                query.Parameters.AddWithValue("@cod_usuario", cod_usuario);
                query.Parameters.AddWithValue("@email", email);
                query.Parameters.AddWithValue("@tipo_usuario", tipo_usuario);
                query.Parameters.AddWithValue("@senha", senha);
                query.Parameters.AddWithValue("@rua", rua);
                query.Parameters.AddWithValue("@cidade", cidade);
                query.Parameters.AddWithValue("@bairro", bairro);
                query.Parameters.AddWithValue("@nome", nome);
                query.Parameters.AddWithValue("@cod_escola", cod_escola);
                query.Parameters.AddWithValue("@cargo", Cargo);

                SqlCommand query2 = new SqlCommand("INSERT INTO Telefone VALUES(@numero, @ddd, @cod_usuario)", con);
                query2.Parameters.AddWithValue("@numero", numero);
                query2.Parameters.AddWithValue("@ddd", ddd);
                query2.Parameters.AddWithValue("@cod_usuario", cod_usuario);

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

        /******************* listar usuarios *********************/
        public static List<Usuario> ListaFuncionarios()
        {
            List<Usuario> listaUsu = new List<Usuario>();

            try

            {
                //abrindo conexão
                con.Open();

                SqlCommand query = new SqlCommand("SELECT * FROM Usuarios where cargo = 1", con);
                SqlDataReader leitor = query.ExecuteReader();
                while (leitor.Read())
                {

                    Usuario u = new Usuario();
                    u.cod_usuario = (int)leitor["cod_usuario"];
                    u.email = leitor["email"].ToString();
                    u.tipo_usuario = leitor["tipo_usuario"].ToString();
                    u.senha = leitor["senha"].ToString();
                    u.rua = leitor["rua"].ToString();
                    u.cidade = leitor["cidade"].ToString();
                    u.nome = leitor["nome"].ToString();
                    u.bairro = leitor["bairro"].ToString();
                    u.nome = leitor["nome"].ToString();
                    u.cod_escola = (int)leitor["cod_escola"];
                    listaUsu.Add(u);
                }
            }
            catch (Exception e)
            {
                listaUsu = new List<Usuario>();
            }

            if (con.State == ConnectionState.Open)

                //fechando conexão
                con.Close();

            return listaUsu;
        }

        public static List<Usuario> ListaAlunos()
        {
            List<Usuario> listaUsu = new List<Usuario>();

            try

            {
                //abrindo conexão
                con.Open();

                SqlCommand query = new SqlCommand("SELECT * FROM Usuarios WHERE cargo = 2", con);
                SqlDataReader leitor = query.ExecuteReader();
                while (leitor.Read())
                {

                    Usuario u = new Usuario();
                    u.cod_usuario = (int)leitor["cod_usuario"];
                    u.email = leitor["email"].ToString();
                    u.tipo_usuario = leitor["tipo_usuario"].ToString();
                    u.senha = leitor["senha"].ToString();
                    u.rua = leitor["rua"].ToString();
                    u.cidade = leitor["cidade"].ToString();
                    u.nome = leitor["nome"].ToString();
                    u.bairro = leitor["bairro"].ToString();
                    u.nome = leitor["nome"].ToString();
                    u.cod_escola = (int)leitor["cod_escola"];
                    listaUsu.Add(u);
                }
            }
            catch (Exception e)
            {
                listaUsu = new List<Usuario>();
            }

            if (con.State == ConnectionState.Open)

                //fechando conexão
                con.Close();

            return listaUsu;
        }

        /******************* alterar dados de usuários *********************/
        internal string Editar()
        {
            string res = "Salvo com sucesso!";

            try
            {
                //abrindo conexão
                con.Open();

                SqlCommand query =
                     new SqlCommand("UPDATE Usuarios SET " +
                     "Cod_usuario = @cod_usuario, Email = @email, " +
                     "Tipo_Usuario = @tipo_usuario, Senha = @senha, Rua = rua, Cidade = @cidade, Bairro = @bairro, Nome = @nome, Cod_escola = @cod_escola, Cargo = @cargo Where Cod_Usuario = @cod_usuario", con);
                query.Parameters.AddWithValue("@cod_usuario", cod_usuario);
                query.Parameters.AddWithValue("@email", email);
                query.Parameters.AddWithValue("@tipo_usuario", tipo_usuario);
                query.Parameters.AddWithValue("@senha", senha);
                query.Parameters.AddWithValue("@rua", rua);
                query.Parameters.AddWithValue("@cidade", cidade);
                query.Parameters.AddWithValue("@bairro", bairro);
                query.Parameters.AddWithValue("@nome", nome);
                query.Parameters.AddWithValue("@cod_escola", cod_escola);
                query.Parameters.AddWithValue("@cargo", Cargo);

                SqlCommand query2 = new SqlCommand("UPDATE Telefone SET Numero = @numero, Ddd = @ddd, Cod_usuario = @cod_usuario Where Cod_Usuario = @cod_usuario", con);
                query2.Parameters.AddWithValue("@numero", numero);
                query2.Parameters.AddWithValue("@ddd", ddd);
                query2.Parameters.AddWithValue("@cod_usuario", cod_usuario);

                query.ExecuteNonQuery();
                query2.ExecuteNonQuery();
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

        /******************* buscar usuários *********************/
        public static Usuario BuscaUsuario(int cod_usuario)
        {
            Usuario li = new Usuario();

            try
            {

                // abrindo conexão
                con.Open();


                SqlCommand query = new SqlCommand("SELECT * FROM Usuarios WHERE cod_usuario = @cod_usuario", con);
                query.Parameters.AddWithValue("@cod_usuario", cod_usuario);
                SqlDataReader leitor = query.ExecuteReader();

                while (leitor.Read())
                {

                    li.Cod_usuario = (int)leitor["cod_usuario"];
                    li.Email = leitor["email"].ToString();
                    li.Tipo_usuario = leitor["tipo_usuario"].ToString();
                    li.Senha = leitor["senha"].ToString();
                    li.Rua = leitor["rua"].ToString();
                    li.Cidade = leitor["cidade"].ToString();
                    li.Bairro = leitor["bairro"].ToString();
                    li.Nome = leitor["nome"].ToString();
                    li.Cod_escola = (int)leitor["cod_escola"];
                    li.Ddd = (int)leitor["ddd"];

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

        /******************* excluir usuarios *********************/
        internal string Remover()
        {
            string res = "Removido com sucesso!";

            try
            {

                //abrindo conexão
                con.Open();

                SqlCommand query =
                     new SqlCommand("DELETE FROM Telefone WHERE Cod_usuario = @cod_usuario", con);
                query.Parameters.AddWithValue("@cod_usuario", cod_usuario);

                SqlCommand query2 =
                   new SqlCommand("DELETE FROM Usuarios WHERE Cod_usuario = @cod_usuario", con);
                query2.Parameters.AddWithValue("@cod_usuario", cod_usuario);

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

        /******************* detalhes de usuários *********************/
        //Dados que não foram exibidos na lista
        public static Usuario detalhes(string det)
        {
            Usuario li = new Usuario();

            try
            {
                //abrindo conexão
                con.Open();

                SqlCommand query = new SqlCommand("SELECT a.cod_usuario, a.email, a.tipo_usuario, a.senha, a.rua, a.cidade, a.bairro, a.nome, a.cod_escola, b.numero, b.ddd FROM Usuarios as " +
                    "A JOIN Telefone as B on  A.cod_usuario = B.cod_usuario", con);
                query.Parameters.AddWithValue("@cod_usuario", det);
                SqlDataReader leitor = query.ExecuteReader();

                while (leitor.Read())
                {

                    li.Cod_usuario = (int)leitor["cod_usuario"];
                    li.Email = leitor["email"].ToString();
                    li.Tipo_usuario = leitor["tipo_usuario"].ToString();
                    li.Senha = leitor["senha"].ToString();
                    li.Rua = leitor["rua"].ToString();
                    li.Cidade = leitor["cidade"].ToString();
                    li.Bairro = leitor["bairro"].ToString();
                    li.Nome = leitor["nome"].ToString();
                    li.Cod_escola = (int)leitor["cod_escola"];
                    li.Numero = (int)leitor["numero"];
                    li.Ddd = (int)leitor["ddd"];

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
    }
}
    
 