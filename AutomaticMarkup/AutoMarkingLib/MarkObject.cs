using System.Drawing;
using System.Text.Json.Serialization;

namespace AutoMarkingLib
{
	public class MarkObject
	{
		public MarkObject(List<Point> points) 
		{ 
			Points = points.Select(x => new MarkPoint(x)).ToList(); 
		}

		[JsonInclude]
		public List<MarkPoint> Points = new();
	}
}
