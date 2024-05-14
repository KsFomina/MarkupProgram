using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticMarkup.Layout.DataBase
{
    public class BaseConnection
    {
        string connectionString = "Data Sourse=DESKTOP-T89823G\\SQLEXPRESS; Database=History; user=root; Integrated Security=True";

        //public BaseConnection()
        //{
        //    MySqlConnectionStringBuilder con = new MySqlConnectionStringBuilder();
        //    con.Server = "localhost";
        //    con.UserID = "root";
        //    con.Database = "History";
        //    con.Password = "";


        //    connectionString = con.ToString();
        //}
        MySqlConnection connection;

        public void openConnection()
        {
            if (connection==null || connection.State==System.Data.ConnectionState.Closed) {

                connection = new MySqlConnection(connectionString);
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
        public MySqlConnection getConnectionString() {
            return connection;
        }

        public DataTable getData()
        {
            return connection.GetSchema("history");
        }
    }
}