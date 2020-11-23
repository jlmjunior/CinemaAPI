using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class DAO
    {
        private string _conn;

        public DAO(string connectionString)
        {
            _conn = connectionString;
        }

        public void ExecuteNonQuery(SqlCommand command)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_conn))
                {
                    connection.Open();

                    using (SqlCommand cmd = command)
                    {
                        cmd.Connection = connection;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExecuteScalar(SqlCommand command)
        {
            int result;

            try
            {
                using (SqlConnection connection = new SqlConnection(_conn))
                {
                    connection.Open();

                    using (SqlCommand cmd = command)
                    {
                        cmd.Connection = connection;

                        result = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
