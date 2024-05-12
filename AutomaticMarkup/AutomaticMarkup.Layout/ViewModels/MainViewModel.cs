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

namespace AutomaticMarkup.ViewModels
{
    internal class MainViewModel : ReactiveObject
    {
        private readonly IRegionManager _regionManager;

        [Reactive] public bool IsFlipped { get; set; }
        public ImageModel SelectedImage { get; set; }
        
        public ICommand UploadFile { get; }
        public ICommand BDHistory { get; }
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

            SelectedImage = imageModel;
        }

        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedImage.Image = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void OpenNewWindow()
        {
            _regionManager.RequestNavigate("HomeRegion", "HistoryRegion");
            IsFlipped = false;

            //var view = new StoryView();
            //var vm = new StoryViewModel();
            //IRegion menuRegion = _regionManager.Regions["MenuRegion"];
            //foreach (var existingView in menuRegion.Views.ToList())
            //{
            //    menuRegion.Remove(existingView);
            //}

            //IRegion homeRegion = _regionManager.Regions["HomeRegion"];
            //homeRegion.Add(view);
            //view.DataContext = vm;

            //_regionManager.Regions["HomeRegion"].Activate(view);
        }


    }
}
