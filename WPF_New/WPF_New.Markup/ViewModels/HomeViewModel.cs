using Microsoft.Win32;
using ReactiveUI;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF_New.ViewModels
{
    internal class HomeViewModel : ReactiveObject
    {
        private ImageSource _selectedImage;
        public ImageSource SelectedImage
        {
            get => _selectedImage;
            set => this.RaiseAndSetIfChanged(ref _selectedImage, value);
        }
        public ICommand UploadFile { get; }

        public HomeViewModel() => UploadFile = ReactiveCommand.Create(UploadFileAsync);

        private void UploadFileAsync()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedImage = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

    }

}
