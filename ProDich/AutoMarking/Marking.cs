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
