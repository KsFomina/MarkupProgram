using AutoMarking;
using System.Drawing;

namespace AutoMarkingTest
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
			var srcImage = @"D:\Projects\ProDich\ProDich\AutoMarkingTest\TestingSD.png";
			var srcMask = @"D:\Projects\ProDich\ProDich\AutoMarkingTest\MaskSD.png";
			srcImage = "TestingSD.png";
			srcMask = "MaskSD.png";
			Bitmap image = new Bitmap(srcImage);
			Bitmap mask = new Bitmap(srcMask);
			var autoMarking = new Marking(srcImage, srcMask);
			//var autoMarking = new Marking(image, mask);
			Console.Read();
		}
	}
}
