using AutomaticMarkup.Layout;
using AutomaticMarkup.Layout.DataBase;
using Prism.Commands;
using Prism.Regions;
using ReactiveUI;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutomaticMarkup.ViewModels
{
    class StoryViewModel : ReactiveObject

    {
        private readonly IRegionManager _regionManager;
        private DataTable dataTable = new DataTable();
        private Object _row;
        public Object row
        {
            get
            {
                return _row;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _row, value);
            }
        }
        public DataTable DataTable
        {
            get
            {
                return dataTable;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref dataTable, value);
            }
        }

        public StoryViewModel()
        {

            //_regionManager = regionManager;
            BaseConnection db;

            try
			{
                db = new BaseConnection();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				db.openConnection();
				string querty = "SELECT * FROM history";
				SqlCommand command = new SqlCommand(querty, db.getConnectionString());
				sqlDataAdapter.SelectCommand = command;
				sqlDataAdapter.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Произошла ошибка при получении данных из базы данных: " + ex.Message);
			}
			finally
			{
				//db.closeConnection();
			}
            GetHistoryMoment = new DelegateCommand(GetMoment);
            //BackCommand = new DelegateCommand(Back);
        }
        public void GetMoment()
        {
            if (row is DataRow dataRow)
            {
                try
            {
                // Проверка, что строка не пуста и имеет хотя бы один элемент
                if (dataRow == null || dataRow.ItemArray.Length == 0 || dataRow[0] == DBNull.Value)
                {
                    throw new ArgumentException("Передана пустая строка или строка без элементов.");
                }

                // Попытка разобрать первый элемент строки как целое число
                int id = int.Parse(dataRow[0].ToString());

                // Дальнейшая логика с использованием переменной id...
            }
            catch (FormatException)
            {
                // Обработка ошибки, если строка не может быть преобразована в число
                Console.WriteLine("Первый элемент строки не является числом.");
            }
            catch (ArgumentException ex)
            {
                // Обработка ошибки, если строка пуста
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                // Обработка других неожиданных ошибок
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
                }
else
{
    // Обработка случая, когда someObject не является DataRow
}
        }
        public ICommand GetHistoryMoment { get; }


    }
}

