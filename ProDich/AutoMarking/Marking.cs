using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarking
{
	public class Marking
	{
		Image<Bgr, byte> Image;
		Image<Bgr, byte> MaskImage;
		Image<Bgr, byte> MarkImage;

		public Marking(Bitmap image, Bitmap Mask)
		{
			Image = image.ToImage<Bgr, byte>();
			MaskImage = Mask.ToImage<Bgr, byte>();
			
			GenerateMark();
		}

		public Marking(string srcImage, string srcMask)
		{
			Image = new Image<Bgr, byte>(srcImage);
			MaskImage = new Image<Bgr, byte>(srcMask);
			
			GenerateMark();
		}

		private void GenerateMark()
		{
			var gray = MaskImage.Convert<Gray, byte>();
			VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
			Mat mat = new Mat();

			CvInvoke.FindContours(gray, contours, mat, RetrType.External, ChainApproxMethod.ChainApproxSimple);

			var mask = gray.CopyBlank();
			for (int i = 0; i < contours.Size; i++)
			{
				var area = CvInvoke.ContourArea(contours[i]);
				var bbox = CvInvoke.BoundingRectangle(contours[i]);
				CvInvoke.DrawContours(mask, contours, i, new MCvScalar(255, 0, 0), 5);

			}
			CvInvoke.Imshow("orig", Image);
			CvInvoke.Imshow("mask", MaskImage);
			CvInvoke.Imshow("gray", gray);
			CvInvoke.Imshow("test", mask);
		}

		public Bitmap GetBitmap()
		{
			return Image.ToBitmap();
		}

		public Bitmap GetMarkBitmap()
		{
			return MarkImage.ToBitmap();
		}

		public Bitmap GetMaskBitmap()
		{
			return MaskImage.ToBitmap();
		}
	}
}
