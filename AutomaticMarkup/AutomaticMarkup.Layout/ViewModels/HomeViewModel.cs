using AutomaticMarkup.Layout.Models;
using ReactiveUI;
using System.Windows.Input;
using System.Windows.Media;

namespace AutomaticMarkup.ViewModels
{
    internal class HomeViewModel : ReactiveObject
    {
        public ImageModel Image { get; set; }
        public HomeViewModel(ImageModel image) 
        { 
            Image = image;

            ImageUp = Image.ImageOrig;
            ImageLeft = Image.ImageMark;
            ImageRight = Image.ImageMask;

            LeftToUPComm = ReactiveCommand.Create(LeftToUP);
            RightToUPComm = ReactiveCommand.Create(RightToUP);
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

        public ICommand LeftToUPComm {  get; set; }
        public ICommand RightToUPComm { get; set; }

        public void LeftToUP() {
            var temp = ImageLeft;
            ImageLeft = ImageUp;
            ImageUp = temp;
        }
        public void RightToUP()
        {
            var temp = ImageRight;
            ImageRight = ImageUp;
            ImageUp = temp;
        }
    }

}
