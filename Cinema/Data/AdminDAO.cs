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

        public List<UsuarioModel> BuscarUsuarios()
        {
            string query = "SELECT usuario, id_role FROM usuarios";

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            DataTable dt = FillDataTable(cmd);

            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                List<UsuarioModel> usuarios = new List<UsuarioModel>();

                foreach (DataRow dr in dt.Rows)
                {
                    usuarios.Add(new UsuarioModel
                    {
                        Usuario = dr["usuario"].ToString(),
                        Token = null,
                        Role = (int)dr["id_role"]
                    });
                }

                return usuarios;
            }
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
    }
}