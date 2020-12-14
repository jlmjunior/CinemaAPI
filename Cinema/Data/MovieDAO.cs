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
    public class MovieDAO : DataAccessObject.DAO
    {
        public MovieDAO() : base(ConfigurationManager.ConnectionStrings["default"].ConnectionString) { }

        public FilmeModel GetMovie(int id)
        {
            string query = "SELECT id, titulo, duracao, sinopse, imagem_link, nota, capa_link, ano_lancamento FROM filmes WHERE id = @id";

            SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            cmd.Parameters.AddWithValue("@id", id);

            DataTable dt = FillDataTable(cmd);

            FilmeModel filme = dt.AsEnumerable().Select(row => new FilmeModel 
            {
                Id = row.Field<int>("id"),
                Titulo = row.Field<string>("titulo"),
                Duracao = row.Field<TimeSpan>("duracao"),
                LinkBackground = row.Field<string>("imagem_link"),
                LinkCapa = row.Field<string>("capa_link"),
                Sinopse = row.Field<string>("sinopse"),
                Nota = row.Field<decimal>("nota"),
                Ano = row["ano_lancamento"] == DBNull.Value ? null : row.Field<int>("ano_lancamento").ToString()
            }).ToList().FirstOrDefault();

            return filme;
        }

        public List<string> GetGenres(int id)
        {
            string query = "SELECT g.genero FROM filmes_generos fg JOIN generos g ON g.id = fg.id_genero WHERE fg.id_filme = @id";

            SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            cmd.Parameters.AddWithValue("@id", id);

            DataTable dt = FillDataTable(cmd);

            List<string> generos = dt.AsEnumerable().Select(row => row.Field<string>("genero")).ToList();

            return generos;
        }

        public List<SessaoModel> GetSessions(int idFilme)
        {
            string query = "SELECT id, id_sala, data_inicio FROM sessoes WHERE id_filme = @idFilme ORDER BY data_inicio";

            SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            cmd.Parameters.AddWithValue("@idFilme", idFilme);

            DataTable dt = FillDataTable(cmd);

            List<SessaoModel> sessoes = dt.AsEnumerable().Select(row => new SessaoModel
            {
                Id = row.Field<int>("id"),
                IdSala = row.Field<int>("id_sala"),
                Horario = row.Field<DateTime>("data_inicio"),
            }).ToList();

            return sessoes;
        }

        public List<AssentoModel> BuscarAssentos(int idSessao)
        {
            string query = "SELECT a.id, a.linha, a.coluna, a.bit_especial, i.id_assento ingresso FROM assentos a   " +
                           "JOIN sessoes s ON s.id_sala = a.id_sala                                 " +
                           "LEFT JOIN ingressos i ON i.id_assento = a.id AND i.id_sessao = s.id     " +
                           "WHERE s.id = @id                                                        ";

            SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            cmd.Parameters.AddWithValue("@id", idSessao);

            DataTable dt = FillDataTable(cmd);

            List<AssentoModel> assentos = dt.AsEnumerable().Select(row => new AssentoModel
            {
                Id = row.Field<int>("id"),
                Linha = row.Field<string>("linha"),
                Coluna = row.Field<int>("coluna"),
                Especial = row.Field<int>("bit_especial"),
                Ocupado = row["ingresso"] != DBNull.Value
            }).ToList();

            return assentos;
        }

        public bool ExisteIngresso(int sessao, int assento)
        {
            string query = "SELECT Count(id) FROM ingressos WHERE id_sessao = @sessao AND id_assento = @assento";

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            cmd.Parameters.AddWithValue("@sessao", sessao);
            cmd.Parameters.AddWithValue("@assento", assento);

            return (ExecuteScalar(cmd) > 0);
        }

        public void ComprarIngresso(IngressoModel ingresso)
        {
            string query = "INSERT INTO ingressos (id_usuario, id_sessao, id_assento, data_criacao)                     " +
                           "VALUES ((SELECT id FROM usuarios WHERE usuario = @usuario), @sessao, @assento, GETDATE())   ";

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            cmd.Parameters.AddWithValue("@usuario", ingresso.Usuario);
            cmd.Parameters.AddWithValue("@sessao", ingresso.IdSessao);
            cmd.Parameters.AddWithValue("@assento", ingresso.IdAssento);

            ExecuteNonQuery(cmd);
        }
    }
}