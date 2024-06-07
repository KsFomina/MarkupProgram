using AutoMarking;
using ReactiveUI;
using System.Windows.Media;

namespace AutomaticMarkup.Layout.Models
{
    internal class ImageModel : ReactiveObject
    {
        public Marking Marking { get; set; }
		private ImageSource _imageMask;
		public ImageSource ImageMask
		{
			get => _imageMask;
			set => this.RaiseAndSetIfChanged(ref _imageMask, value);
		}

        private ImageSource _imageOrig;
        public ImageSource ImageOrig
        {
            get => _imageOrig;
            set => this.RaiseAndSetIfChanged(ref _imageOrig, value);
        }

        private ImageSource _imageMark;
        public ImageSource ImageMark
        {
            get => _imageMark;
            set => this.RaiseAndSetIfChanged(ref _imageMark, value);
        }

    }
}
