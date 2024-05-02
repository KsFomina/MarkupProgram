using Prism.Mvvm;
using AutomaticMarkup.Layout.Models;

namespace AutomaticMarkup.ViewModels
{
    internal class HomeViewModel : BindableBase
    {
        public ImageModel Image {get; set;}
        public HomeViewModel(ImageModel image) { Image = image; }
    }

}
