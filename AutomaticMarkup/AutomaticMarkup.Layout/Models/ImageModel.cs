using ReactiveUI;
using System.Windows.Media;

namespace AutomaticMarkup.Layout.Models
{
    internal class ImageModel : ReactiveObject
    {
		private ImageSource _image;
		public ImageSource Image
		{
			get => _image;
			set => this.RaiseAndSetIfChanged(ref _image, value);
		}
	}
}
