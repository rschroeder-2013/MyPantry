using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    internal static class DBConnection
    {
        private static string connectionString =
            @"Data Source=localhost;Initial Catalog=my_pantry_db2;Integrated Security=True";

        public static SqlConnection GetDBConnection()
        {
            var conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
