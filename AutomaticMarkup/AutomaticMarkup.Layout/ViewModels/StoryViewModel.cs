using AutomaticMarkup.Layout;
using AutomaticMarkup.Layout.DataBase;
using Prism.Commands;
using Prism.Regions;
using ReactiveUI;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Input;

namespace AutomaticMarkup.ViewModels
{
    class StoryViewModel: ReactiveObject

    {
        //private readonly IRegionManager _regionManager;
        private DataTable dataTable = new DataTable();
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
            BaseConnection db = new BaseConnection();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            try
            {
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
                db.closeConnection();
            }

            //BackCommand = new DelegateCommand(Back);
        }


        public ICommand BackCommand { get; }

        //private void Back()
        //{
        //    _regionManager
        //        .RequestNavigate(Regions.MainRegion, Navigation.GenerationPage);
        //}
    }
}

