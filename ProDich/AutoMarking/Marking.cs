using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

			var whImg = new Image<Bgr, byte>(gray.Width, gray.Height, new Bgr(Color.White));
			CircleF[] circles = CvInvoke.HoughCircles(gray, HoughModes.Gradient, 9, 1, 100, 100, 0, 25);
			foreach (CircleF circle in circles)
				whImg.Draw(circle, new Bgr(Color.Red), 2);
			CvInvoke.Imshow("circle", whImg);
			CvInvoke.FindContours(gray, contours, mat, RetrType.List, ChainApproxMethod.ChainApproxSimple);
			
			var mask = gray.CopyBlank();

			foreach (CircleF circle in circles)
				Image.Draw(circle, new Bgr(Color.Red), 2);
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

		private void testKernel(Image<Gray, byte> src, Image<Gray, byte> dst)
		{
			Matrix<float> kernel = new Matrix<float>(new float[3, 3] { { -0.1f, -0.1f, -0.1f }, { -0.1f, 2f, -0.1f }, { -0.1f, -0.1f, -0.1f } });			CvInvoke.Filter2D(src, dst, kernel, new Point(-1, -1));
		}
	}
}
