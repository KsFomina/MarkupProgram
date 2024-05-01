using Microsoft.Win32;
using Prism.Events;
using Prism.Regions;
using ReactiveUI;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPF_New.ViewModels;


namespace WPF_New.Markup.ViewModels
{
    class MainWindowViewModel:ReactiveObject
    {
       public HomeViewModel HomeViewModel { get; set; }
       public MainViewModel MainViewModel { get; set; }


    }
}
