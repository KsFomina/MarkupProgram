using AutomaticMarkup.Layout;
using AutomaticMarkup.Layout.DataBase;
using Prism.Commands;
using Prism.Regions;
using ReactiveUI;
using System.ComponentModel;
using System.Data;
using System.Windows.Input;

namespace AutomaticMarkup.ViewModels
{
    class StoryViewModel: ReactiveObject

    {
        private readonly IRegionManager _regionManager;
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
            BaseConnection db = new BaseConnection();
            db.openConnection();
            dataTable=db.getData();
            db.closeConnection();
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
    }
}

