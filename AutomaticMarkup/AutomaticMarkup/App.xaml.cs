using System.Windows;
using AutomaticMarkup.Layout;
using Prism.Ioc;
using Prism.Modularity;
using AutomaticMarkup.Layout.Views;
using AutomaticMarkup.Layout.ViewModels;
using AutomaticMarkup.Views;

namespace AutomaticMarkup
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HomeView>();
            containerRegistry.RegisterForNavigation<StoryView>();
        }


        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog
                .AddModule<MarkupModul>();

        }

        protected override Window CreateShell() => Container.Resolve<MainWindow>();

    }

}
