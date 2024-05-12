using AutomaticMarkup.Layout.Models;
using ReactiveUI;

namespace AutomaticMarkup.ViewModels
{
    internal class HomeViewModel : ReactiveObject
    {
        public ImageModel Image { get; set; }
        public HomeViewModel(ImageModel image) 
        { 
            Image = image; 
        }
    }

}
