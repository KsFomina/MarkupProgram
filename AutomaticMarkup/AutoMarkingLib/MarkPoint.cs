using System.Text.Json.Serialization;

namespace AutoMarkingLib
{
	public class MarkPoint
	{
		public MarkPoint(System.Drawing.Point p)
		{
			X = p.X; 
			Y = p.Y;
		}
		[JsonInclude]
		public int X;
		[JsonInclude]
		public int Y;
	}
}
