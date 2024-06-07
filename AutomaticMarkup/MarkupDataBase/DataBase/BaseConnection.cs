using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace MarkupDataBase.DataBase
{
    public class BaseConnection
    {
        string connectionString = "Server=.\\SQLEXPRESS;Database=History; Integrated Security=True";

        SqlConnection connection;

        public void openConnection()
        {
            if (connection == null || connection.State == System.Data.ConnectionState.Closed)
            {

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
        public SqlConnection getConnectionString()
        {
            return connection;
        }


        public void AddData(string name_file, DateTime time_create, DateTime data_create, byte[] file_marking, byte[] file_source, byte[] file_mask, byte[] marking)
        {
            // SQL-запрос для вставки данных
            string query = "INSERT INTO history (name_file, time_create, date_create, file_marking, file_source, file_mask, marki) " +
                "VALUES (@name_file, @time_create, @date_create, @file_marking, @file_source, @file_mask, @marki)";

                // Создание объекта команды
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Добавление параметров в команду для защиты от SQL-инъекций
                    command.Parameters.AddWithValue("@name_file", name_file);
                    command.Parameters.AddWithValue("@time_create", time_create);
                    command.Parameters.AddWithValue("@date_create", data_create);
                    command.Parameters.AddWithValue("@file_marking", file_marking);
                    command.Parameters.AddWithValue("@file_source", file_source);
                    command.Parameters.AddWithValue("@file_mask", file_mask);
                    command.Parameters.AddWithValue("@marki", marking);

                // Открытие соединения
                openConnection();

                // Выполнение команды
                int result = command.ExecuteNonQuery();

                // Проверка результата
                if (result < 0)
                    throw new Exception("ошибка при добавлении новой записи");
                else
                    closeConnection();
                }
            
        }
        public (byte[] DataOrig, byte[] DataMark, byte[] DataMask ) GetHistory(int Id)
        {
            byte[] DataOrig = new byte[] { };
            byte[] DataMark = new byte[] { };
            byte[] DataMask = new byte[] { };
            openConnection();
            string sql = "SELECT * FROM history WHERE id=@Id";

            using (SqlCommand command = new SqlCommand(sql, connection))

            {
                command.Parameters.AddWithValue("@Id", Id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataOrig = (byte[])reader.GetValue(5);
                        DataMark = (byte[])reader.GetValue(4);
                        DataMask = (byte[])reader.GetValue(6);
                    }
                }
            }
            closeConnection();
            return (DataOrig, DataMark, DataMask);
        }
        public void start(DataTable dataTable)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            string querty = "SELECT id,name_file,time_create,date_create FROM history";
            SqlCommand command = new SqlCommand(querty, connection);
            sqlDataAdapter.SelectCommand = command;
            sqlDataAdapter.Fill(dataTable);
        }

    }
}