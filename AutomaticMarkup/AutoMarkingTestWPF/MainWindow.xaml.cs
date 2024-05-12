using AutoMarking;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestWPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			var srcImage = @"TestingSD.png";
			var srcMask = @"MaskSD.png";
			Bitmap image = new Bitmap(srcImage);
			Bitmap mask = new Bitmap(srcMask);
			var autoMarking = new Marking(srcImage, srcMask);
			DataContext = new MainWindowViewModel();
		}
	}
}