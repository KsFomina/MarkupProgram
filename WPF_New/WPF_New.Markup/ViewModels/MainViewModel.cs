using ReactiveUI;
using System.Windows.Input;
using Microsoft.Win32;
using Prism.Events;
using Prism.Regions;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF_New.ViewModels
{
    internal class MainViewModel : ReactiveObject
    {
        private readonly IRegionManager _regionManager;

        private ImageSource _selectedImage;
        public ImageSource SelectedImage
        {
            get => _selectedImage;
            set => this.RaiseAndSetIfChanged(ref _selectedImage, value);
        }
        public ICommand UploadFile { get; }

        public MainViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            var eventAggregator1 = eventAggregator;
            _regionManager = regionManager;

            UploadFile = ReactiveCommand.Create(OpenFile);

        }

        private void OpenFile()
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
