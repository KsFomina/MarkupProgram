using AutomaticMarkup.Layout.Models;
using ReactiveUI;
using System.Windows.Input;
using System.Windows.Media;

namespace AutomaticMarkup.ViewModels
{
    internal class HomeViewModel : ReactiveObject
    {
        private ImageModel _image;

		public ImageModel Image 
        { 
            get => _image;
            set
            {
				this.RaiseAndSetIfChanged(ref _image, value);
				ImageUp = Image.ImageOrig;
				ImageLeft = Image.ImageMark;
				ImageRight = Image.ImageMask;
			} 
        }
        public HomeViewModel(ImageModel image) 
        { 
            Image = image;

            ImageUp = Image.ImageOrig;
            ImageLeft = Image.ImageMark;
            ImageRight = Image.ImageMask;
        }

        private ImageSource _imageUp;
        public ImageSource ImageUp
        {
            get => _imageUp;
            set => this.RaiseAndSetIfChanged(ref _imageUp, value);
        }

        private ImageSource _imageLeft;
        public ImageSource ImageLeft
        {
            get => _imageLeft;
            set => this.RaiseAndSetIfChanged(ref _imageLeft, value);
        }

        private ImageSource _imageRight;
        public ImageSource ImageRight
        {
            get => _imageRight;
            set => this.RaiseAndSetIfChanged(ref _imageRight, value);
        }
    }

}
