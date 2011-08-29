using System;
using NUnit.Framework;
using System.Drawing;

namespace Life.Graphics.Tests
{
	[TestFixture]
	public class ColorExtensionsTests
	{
		[Test]
		public void ToRgba_ConvertsColors( )
		{
			Assert.AreEqual( 0xFFFFFFFF, Color.White.ToRgba( ), "Does not convert white properly" );
			Assert.AreEqual( 0xFF000000, Color.Black.ToRgba( ), "Does not convert black properly" );
			Assert.AreEqual( 0xFF0000FF, Color.Red.ToRgba( ), "Does not convert red properly" );
			Assert.AreEqual( 0xFF00FF00, Color.FromArgb( 0, 255, 0 ).ToRgba( ), "Does not convert green properly" );
			Assert.AreEqual( 0xFFFF0000, Color.Blue.ToRgba( ), "Does not convert blue properly" );
		}
	}
}

