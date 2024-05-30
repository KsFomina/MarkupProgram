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
        public ICommand DataBaseClickComand { get; }
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
            BaseConnection db = new BaseConnection();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            db.openConnection();
            string querty = "SELECT * FROM history";
            SqlCommand command = new SqlCommand(querty, db.getConnectionString());
            sqlDataAdapter.SelectCommand = command;
            sqlDataAdapter.Fill(dataTable);
            db.closeConnection();
            DataBaseClickComand = new DelegateCommand(DataBaseClick);
        }

        public StoryViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            BackCommand = new DelegateCommand(Back);
        }

        public ICommand BackCommand { get; }

        private void Back()
        {
            _regionManager
                .RequestNavigate(Regions.MainRegion, Navigation.GenerationPage);
        }

        private void DataBaseClick()
        {
            //if (DataGrid.CurrentRow)
            //{
            //    // Получаем ID выбранного элемента
            //    var selectedRow = dataGridView1.SelectedRows[0];
            //    int itemId = Convert.ToInt32(selectedRow.Cells["ID"].Value);
            //}
        }
    }
}

