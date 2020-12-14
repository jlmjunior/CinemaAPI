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
            string query = "SELECT usuario, id_role, data_criacao FROM usuarios WHERE id_role <> 1";

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            return FillDataTable(cmd);
        }

        public DataTable BuscarSessoes()
        {
            string query = "SELECT s.id, f.titulo, s.id_sala, s.data_inicio FROM sessoes s JOIN filmes f ON f.id = s.id_filme ORDER BY s.data_inicio";

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

        public void CadastrarSessao(CadastroSessaoModel sessao)
        {
            string query = "INSERT INTO sessoes (id_filme, id_sala, data_inicio, data_fim, data_criacao) " +
                           "VALUES (@filme, @sala, @inicio, @fim, GETDATE())";

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            cmd.Parameters.AddWithValue("@filme", sessao.IdFilme);
            cmd.Parameters.AddWithValue("@sala", sessao.IdSala);
            cmd.Parameters.AddWithValue("@inicio", sessao.DataInicio);
            cmd.Parameters.AddWithValue("@fim", sessao.DataFim);

            ExecuteNonQuery(cmd);
        }

        public DataTable BuscarFilmes()
        {
            string query = "SELECT id, titulo FROM filmes";

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            return FillDataTable(cmd);
        }

        public DataTable BuscarSalas()
        {
            string query = "SELECT id, descricao FROM salas";

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            return FillDataTable(cmd);
        }

        public bool HasAdminToken(string token)
        {
            string query = "SELECT Count(id) FROM usuarios WHERE token = @token AND id_role = 1";

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            cmd.Parameters.AddWithValue("@token", token);

            return (ExecuteScalar(cmd) > 0);
        }
    }
}