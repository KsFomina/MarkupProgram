using Microsoft.Win32;
using Prism.Mvvm;
using ReactiveUI;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPF_New.Markup.Models;
using WPF_New.Models;

namespace WPF_New.ViewModels
{
    internal class HomeViewModel : BindableBase
    {
        public ImageModel Image {get; set;}
        public HomeViewModel(ImageModel image) { Image = image; }
    }

}
