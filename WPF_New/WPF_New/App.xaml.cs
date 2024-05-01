using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.ComponentModel;
using System.Windows;
using WPF_New.Markup;
using WPF_New.ViewModels;
using WPF_New.Views;



namespace WPF_New
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {


        }
       
       
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<MarkupModul>();
        }

        protected override Window CreateShell() => Container.Resolve<MainWindow>();

    }

}
