using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using AutomaticMarkup.Views;
using AutomaticMarkup.ViewModels;
using AutomaticMarkup.Layout.Views;

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
               .RegisterViewWithRegion("HistoryRegion", nameof(StoryView))
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

            containerRegistry
          .RegisterForNavigation<StoryView>();

        }
    }
}
