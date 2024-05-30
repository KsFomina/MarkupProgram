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
using AutomaticMarkup.Layout.DataBase;
using AutomaticMarkup.Views;


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

        public ICommand Undo { get; }
        string file_name;
        public BaseConnection BaseConnection { get; set; }

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
            Undo = new DelegateCommand(OpenOldWindow);
            SelectedImage = imageModel;
            BaseConnection=new BaseConnection();
            BaseConnection.openConnection();
        }

        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedImage.ImageOrig = new BitmapImage(new Uri(openFileDialog.FileName));
                file_name = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
            }

			if (openFileDialog.ShowDialog() == true)
			{
				SelectedImage.ImageMask = new BitmapImage(new Uri(openFileDialog.FileName));
			}
		}

        private void OpenNewWindow()
        {
            //_regionManager.RequestNavigate("HomeRegion", "StoryView");
            //IsFlipped = false;

            var view = new StoryView();
            var vm = new StoryViewModel();
            IRegion menuRegion = _regionManager.Regions["MenuRegion"];
            IRegion homeRegion = _regionManager.Regions["HomeRegion"];
            homeRegion.Add(view);
            view.DataContext = vm;

            _regionManager.Regions["HomeRegion"].Activate(view);
        }

        public void OpenOldWindow()
        {
            _regionManager.RequestNavigate("StoryRegion", nameof(MainView));
            IsFlipped = false;
        }
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        private async void AutoMarking()
        {
			Marking marking = null;
			Bitmap orig = ConvertToBitmap((BitmapSource)SelectedImage.ImageOrig);
			Bitmap mask = ConvertToBitmap((BitmapSource)SelectedImage.ImageMask);

			await Task.Run(() => { marking = GetMark(orig, mask); });

			SelectedImage.ImageOrig = ConvertToImageSource(marking.GetBitmap());
            SelectedImage.ImageMark = ConvertToImageSource(marking.GetMarkBitmap());
            SelectedImage.ImageMask = ConvertToImageSource(marking.GetMaskBitmap());
            DateTime  time= DateTime.Now;
            var img1 = ImageToByte(marking.GetBitmap());
            var img2 = ImageToByte(marking.GetMarkBitmap());
            BaseConnection.AddData(file_name,time,time.Date, img2, img1);

        }

        private Marking GetMark(Bitmap Orig, Bitmap Mask)
        {
			var autoMark = new Marking(Orig, Mask);
            return autoMark;
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
