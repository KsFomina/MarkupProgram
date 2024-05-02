using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using AutomaticMarkup.Views;
using AutomaticMarkup.ViewModels;

namespace AutomaticMarkup.Layout
{
    public class MarkupModul : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider
               .Resolve<IRegionManager>()
               .RegisterViewWithRegion("MenuRegion", nameof(MainView))
               .RegisterViewWithRegion("HomeRegion", nameof(HomeView))
               ;

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry
            .RegisterSingleton<MainViewModel>();

            containerRegistry
           .RegisterForNavigation<MainView>();

            containerRegistry
          .RegisterForNavigation<HomeView>();

        }
    }
}
