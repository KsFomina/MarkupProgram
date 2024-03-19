using MahApps.Metro.Behaviors;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using WPFExample1.Models;
using WPFExample1.Views;
using WPFExample1.Module;

namespace WPFExample1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell() => Container.Resolve<Window>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry
            .RegisterSingleton<Window>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<MainModule>();
        }
    }
}
