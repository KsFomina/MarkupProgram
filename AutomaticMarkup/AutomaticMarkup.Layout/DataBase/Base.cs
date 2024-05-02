using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticMarkup.Layout.DataBase
{
    public class Base
    {
        string connectionString = "Server=DESKTOP-T89823G\\SQLEXPRESS;Database=master;Trusted_Connection=True;";
        MySqlConnection connection = new MySqlConnection("connectionString");
        public async void connectionDB()
        {
            await connection.OpenAsync();
        }
        public Base() { }
    }
}