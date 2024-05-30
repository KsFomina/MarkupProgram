
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticMarkup.Layout.DataBase
{
    public class BaseConnection
    {
        string connectionString = "Server=\\--;Database=History; Integrated Security=True";

        SqlConnection connection;

        public void openConnection()
        {
            if (connection==null || connection.State==System.Data.ConnectionState.Closed) {

                connection = new SqlConnection(connectionString);
                connection.Open();

            }
        }
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public SqlConnection getConnectionString() {
            return connection;
        }

        public DataTable getData()
        {
            return connection.GetSchema("history");
        }
    }
}