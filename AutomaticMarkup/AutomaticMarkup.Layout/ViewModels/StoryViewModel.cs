using AutomaticMarkup.Layout;
using Prism.Commands;
using Prism.Regions;
using ReactiveUI;
using System.Windows.Input;

namespace AutomaticMarkup.ViewModels
{
    class StoryViewModel: ReactiveObject
    {
        private readonly IRegionManager _regionManager;

        public StoryViewModel()
        {
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

