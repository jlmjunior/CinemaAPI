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
            string query = "INSERT INTO usuarios (usuario, senha, data_criacao, id_role) VALUES (@usuario, @senha, GETDATE(), 2)";

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

        public UsuarioModel BuscarUsuario(string usuario, string hashSenha)
        {
            string query = "SELECT usuario, token, id_role AS role FROM usuarios WHERE usuario = @usuario and senha = @senha";

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@senha", hashSenha);

            DataTable dt = FillDataTable(cmd);

            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return new UsuarioModel
                {
                    Usuario = dt.Rows[0]["usuario"].ToString(),
                    Token = dt.Rows[0]["token"] == DBNull.Value ? null : dt.Rows[0]["token"].ToString(),
                    Role = (int)dt.Rows[0]["role"]
                };
            }
        }

        public void InserirToken(string usuario, string token)
        {
            string query = "UPDATE usuarios SET token = @token WHERE usuario = @usuario";

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@token", token);

            ExecuteNonQuery(cmd);
        }
    }
}