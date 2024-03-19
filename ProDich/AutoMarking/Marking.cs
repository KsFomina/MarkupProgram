using Emgu.CV;
using Emgu.CV.Structure;
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

        public Marking(Bitmap bitmap)
        {
            Image = bitmap.ToImage<Bgr, byte>();
        }

		private void GenerateMark()
		{

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
