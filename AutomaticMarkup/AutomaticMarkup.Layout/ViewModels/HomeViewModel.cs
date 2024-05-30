using AutomaticMarkup.Layout.Models;
using Prism.Commands;
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
			} 
        }

		public ICommand ImageRightClickCommand { get; }
		public ICommand ImageLeftClickCommand { get; }

        public HomeViewModel(){ }
            public HomeViewModel(ImageModel image) 
        { 
            Image = image;
			Image.PropertyChanged += Image_PropertyChanged;

            ImageLeftClickCommand = new DelegateCommand(ImageLeftClick);
            ImageRightClickCommand = new DelegateCommand(ImageRightClick);
        }

		private void Image_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			ImageUp = Image.ImageOrig;
			ImageLeft = Image.ImageMask;
			ImageRight = Image.ImageMark;
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

		private void ImageRightClick()
		{

			var temp = ImageRight;
			ImageRight = ImageUp;
			ImageUp = temp;
		}

		private void ImageLeftClick()
		{
			var temp = ImageLeft;
			ImageLeft = ImageUp;
			ImageUp = temp;
		}
	}

}
