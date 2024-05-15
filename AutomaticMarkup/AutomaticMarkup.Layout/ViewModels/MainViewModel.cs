using ReactiveUI;
using System.Windows.Input;
using Microsoft.Win32;
using Prism.Events;
using Prism.Regions;
using System.Windows.Media.Imaging;
using AutomaticMarkup.Layout.Models;
using Prism.Commands;
using AutomaticMarkup.Layout.Views;
using DynamicData.Binding;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Linq;
using AutomaticMarkup.Layout.Events;
using AutomaticMarkup.Layout;
using AutoMarking;
using System.Runtime.InteropServices;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using System.IO;
using System.Drawing.Imaging;


namespace AutomaticMarkup.ViewModels
{
    internal class MainViewModel : ReactiveObject
    {
        private readonly IRegionManager _regionManager;

        [Reactive] public bool IsFlipped { get; set; }
        public ImageModel SelectedImage { get; set; }
        
        public ICommand UploadFile { get; }
        public ICommand BDHistory { get; }
        public ICommand DoMarkUp { get; }
        public ICommand SaveFile {  get; }


        public MainViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, ImageModel imageModel)
        {
            var eventAggregator1 = eventAggregator;
            _regionManager = regionManager;
          
            this.WhenValueChanged(x => x.IsFlipped)
                .Subscribe(x =>
                    eventAggregator1.GetEvent<IsFlippedChanged>()
                        .Publish(x));


            BDHistory = new DelegateCommand(OpenNewWindow);
            UploadFile = ReactiveCommand.Create(OpenFile);
            DoMarkUp = new DelegateCommand(AutoMarking);
            SaveFile = ReactiveCommand.Create(SaveImg);

            SelectedImage = imageModel;
        }

        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedImage.ImageOrig = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void OpenNewWindow()
        {
            //_regionManager.RequestNavigate("HomeRegion", "HistoryRegion");
            //IsFlipped = false;

            var view = new StoryView();
            var vm = new StoryViewModel();
            IRegion menuRegion = _regionManager.Regions["MenuRegion"];
            foreach (var existingView in menuRegion.Views.ToList())
            {
                menuRegion.Remove(existingView);
            }

            IRegion homeRegion = _regionManager.Regions["HomeRegion"];
            homeRegion.Add(view);
            view.DataContext = vm;

            _regionManager.Regions["HomeRegion"].Activate(view);
        }

        private void AutoMarking()
        {
            Bitmap bitmap = ConvertToBitmap((BitmapSource)SelectedImage.ImageOrig);
            var autoMark = new Marking(bitmap, bitmap);
            SelectedImage.ImageOrig = ConvertToImageSource(autoMark.GetMarkBitmap());
        }

		public static Bitmap ConvertToBitmap(BitmapSource bitmapSource)
		{
			var width = bitmapSource.PixelWidth;
			var height = bitmapSource.PixelHeight;
			var stride = width * ((bitmapSource.Format.BitsPerPixel + 7) / 8);
			var memoryBlockPointer = Marshal.AllocHGlobal(height * stride);
			bitmapSource.CopyPixels(new Int32Rect(0, 0, width, height), memoryBlockPointer, height * stride, stride);
			var bitmap = new Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format32bppPArgb, memoryBlockPointer);
			return bitmap;
		}

		BitmapImage ConvertToImageSource(Bitmap bitmap)
		{
			using (MemoryStream memory = new MemoryStream())
			{
				bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
				memory.Position = 0;
				BitmapImage bitmapimage = new BitmapImage();
				bitmapimage.BeginInit();
				bitmapimage.StreamSource = memory;
				bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapimage.EndInit();

				return bitmapimage;
			}
		}


        public void SaveImg()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Files ( * .png)| * .png|JPEG Files ( * .jpeg)| * .jpeg|BMP Files ( * .bmp)| * .bmp|All Files ( * . * )| * . * ",
                Title = "Save Image As..."
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                var imageSource = SelectedImage.ImageOrig;
                string filePathToSave = saveFileDialog.FileName;

                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)SelectedImage.ImageOrig));
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    encoder.Save(stream);
                }

            }
        }

    }
}
