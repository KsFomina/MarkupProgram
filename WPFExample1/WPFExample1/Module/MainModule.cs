using Prism.Modularity;
using Prism.Ioc;
using Prism.Regions;
using System;
using WPFExample1.Views;

namespace WPFExample1.Module
{
    internal class MainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var region = containerProvider.Resolve<IRegionManager>();
            region.RegisterViewWithRegion("MainRegion", typeof(TabView));  //TabView is the view or UserControl which will inject to Region
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
