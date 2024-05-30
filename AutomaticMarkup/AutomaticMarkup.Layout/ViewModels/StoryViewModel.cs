using AutomaticMarkup.Layout.DataBase;
using AutomaticMarkup.Layout.Models;
using Prism.Commands;
using Prism.Regions;
using ReactiveUI;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace AutomaticMarkup.ViewModels
{
    class StoryViewModel : ReactiveObject

    {
        private readonly IRegionManager _regionManager;
        private DataTable dataTable = new DataTable();
        private ImageModel Image=new ImageModel();  
        private int _row;
        public int row
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

        public StoryViewModel(ImageModel image)
        {

            //_regionManager = regionManager;
            BaseConnection db;
            Image = image;
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
        public static ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }
        public void GetMoment()
        {
                try
            {
                // Проверка, что строка не пуста и имеет хотя бы один элемент
                if (DataTable.Rows[row] == null || DataTable.Rows[row].ItemArray.Length == 0 || DataTable.Rows[row][0] == DBNull.Value)
                {
                    throw new ArgumentException("Передана пустая строка или строка без элементов.");
                }

                Image.ImageOrig = ByteToImage((byte[])DataTable.Rows[row][5]);
                Image.ImageMark = ByteToImage((byte[])DataTable.Rows[row][4]);
                Image.ImageMask = ByteToImage((byte[])DataTable.Rows[row][6]);



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
        public ICommand GetHistoryMoment { get; }


    }
}

