using MarkupDataBase.DataBase;
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
using System.Windows;

namespace AutomaticMarkup.ViewModels
{
    class StoryViewModel : ReactiveObject

    {
        BaseConnection db;
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
            
            db = new BaseConnection();
            db.openConnection();
            Image = image;
            try
			{
                db.start(DataTable);

			}
			catch (Exception ex)
			{
                MessageBoxResult dialogResult = MessageBox.Show("Произошла ошибка при получении данных из базы данных: " + ex.Message, "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
			finally
			{
                db.closeConnection();
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
                int Id = (int) DataTable.Rows[row][0];
                var im = db.GetHistory(Id);
                Image.ImageOrig=ByteToImage(im.DataOrig);
                Image.ImageMark = ByteToImage(im.DataMark);
                Image.ImageMask = ByteToImage(im.DataMask);
            }
            catch (FormatException)
            {
                // Обработка ошибки, если строка не может быть преобразована в число
                MessageBoxResult dialogResult = MessageBox.Show("Первый элемент строки не является числом.", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException ex)
            {
                // Обработка ошибки, если строка пуста
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                // Обработка других неожиданных ошибок
                MessageBoxResult dialogResult = MessageBox.Show(ex.Message, "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                
        }
        public ICommand GetHistoryMoment { get; }


    }
}

