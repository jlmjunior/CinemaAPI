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
    public class AdminDAO : DataAccessObject.DAO
    {
        public AdminDAO() : base(ConfigurationManager.ConnectionStrings["default"].ConnectionString) { }

        public DataTable BuscarUsuarios()
        {
            string query = "SELECT usuario, id_role, data_criacao FROM usuarios";

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            return FillDataTable(cmd);
        }

        public DataTable BuscarSessoes()
        {
            string query = "SELECT s.id, f.titulo, s.id_sala, s.data_inicio FROM sessoes s JOIN filmes f ON f.id = s.id_filme";

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            return FillDataTable(cmd);
        }

        public void DeletarUsuario(string usuario)
        {
            string query = "DELETE FROM usuarios WHERE usuario = @usuario";

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            cmd.Parameters.AddWithValue("@usuario", usuario);

            ExecuteNonQuery(cmd);
        }

        public void DeletarSessao(int id)
        {
            string query = "DELETE FROM sessoes WHERE id = @id";

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            cmd.Parameters.AddWithValue("@id", id);

            ExecuteNonQuery(cmd);
        }
    }
}