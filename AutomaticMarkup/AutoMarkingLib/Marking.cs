using AutoMarkingLib;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Drawing;
using System.Text.Json;

namespace AutoMarking
{
	public class Marking
	{
		Image<Bgr, byte> _image;
		Image<Bgr, byte> _maskImage;
		Image<Bgr, byte> _markImage;
		List<MarkObject> _markObjects = new();
		string _jsonMarkObjects;

		public Marking(Bitmap image, Bitmap Mask)
		{
			_image = image.ToImage<Bgr, byte>();
			_maskImage = Mask.ToImage<Bgr, byte>();
			_markImage = _image.Copy();
			GenerateMark();
		}

		public Marking(string srcImage, string srcMask)
		{
			_image = new Image<Bgr, byte>(srcImage);
			_maskImage = new Image<Bgr, byte>(srcMask);
			_markImage = _image.Copy();
			GenerateMark();
		}

		private void GenerateMark()
		{
			var gray = _maskImage.Convert<Gray, byte>();
			CvInvoke.Threshold(gray, gray, 40, 255, ThresholdType.Binary);

			VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
			Mat mat = new Mat();
			CvInvoke.FindContours(gray, contours, mat, RetrType.List, ChainApproxMethod.ChainApproxSimple);

			for (int i = 0; i < contours.Size; i++)
			{
				if (contours[i].Length < 100 || contours[i].Length > 500)
				{
					continue;
				}
				//var area = CvInvoke.ContourArea(contours[i]);
				//var bbox = CvInvoke.BoundingRectangle(contours[i]);
				CvInvoke.DrawContours(_markImage, contours, i, new MCvScalar(255, 0, 0), 5);

				_markObjects.Add(new (contours[i].ToArray().ToList()));
			}
			_jsonMarkObjects = JsonSerializer.Serialize(_markObjects);
		}

		public Bitmap GetBitmap()
		{
			return _image.ToBitmap();
		}

		public Bitmap GetMarkBitmap()
		{
			return _markImage.ToBitmap();
		}

		public Bitmap GetMaskBitmap()
		{
			return _maskImage.ToBitmap();
		}

		public List<MarkObject> GetMarkObjects()
		{
			return _markObjects;
		}

		public string GetJsonMarkObjects()
		{
			return _jsonMarkObjects;
		}

		public void SaveJson(string path)
		{
			var stream = new FileStream(path, FileMode.CreateNew);
			JsonSerializer.Serialize(stream, _markObjects);
		}

		private void testKernel(Image<Gray, byte> src, Image<Gray, byte> dst)
		{
			Matrix<float> kernel = new Matrix<float>(new float[3, 3] { { -0.1f, -0.1f, -0.1f }, { -0.1f, 2f, -0.1f }, { -0.1f, -0.1f, -0.1f } });			CvInvoke.Filter2D(src, dst, kernel, new Point(-1, -1));
		}

		
	}
}
