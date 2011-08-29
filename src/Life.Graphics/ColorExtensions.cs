using System;
using System.Drawing;

namespace Life.Graphics
{
	public static class ColorExtensions
	{
		public static uint ToRgba( this Color color )
		{
			return (uint)color.A << 24 | (uint)color.B << 16 | (uint)color.G << 8 | (uint)color.R;
		}
	}
}

