using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Cinema.Data
{
    public class AuthDAO : DataAccessObject.DAO
    {
        public AuthDAO() : base(ConfigurationManager.ConnectionStrings["default"].ConnectionString) { }

        public void Cadastrar(string usuario, string hashSenha)
        {
            string query = "INSERT INTO usuarios (usuario, senha, data_criacao) VALUES (@usuario, @senha, GETDATE())";

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@senha", hashSenha);

            ExecuteNonQuery(cmd);
        }

        public bool ExisteUsuario(string usuario)
        {
            string query = "SELECT Count(id) FROM usuarios WHERE usuario = @usuario";

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            cmd.Parameters.AddWithValue("@usuario", usuario);

            return (ExecuteScalar(cmd) > 0);
        }

        public bool ValidarLogin(string usuario, string hashSenha)
        {
            string query = "SELECT Count(id) FROM usuarios WHERE usuario = @usuario and senha = @senha";

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@senha", hashSenha);

            return (ExecuteScalar(cmd) > 0);
        }
    }
}