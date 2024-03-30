using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Data.SqlClient;

namespace ProDich.База_данных
{
    public class DataBase
    {
        string connectionString = "Server=DESKTOP-T89823G\\SQLEXPRESS;Database=master;Trusted_Connection=True;";
        MySqlConnection connection = new MySqlConnection("connectionString");
        public async void connectionDB()
        {
            await connection.OpenAsync();
        }
        public DataBase() { }

    }
}
