using System;
using System.Linq;
using NUnit.Framework;
using FakeItEasy;
using System.Drawing;

namespace Life.Platform.Tests
{
	[TestFixture]
	public class DisplayCapabilitiesTests
	{
		[Test]
		public void Ctor_SetsValues( )
		{
			var bounds = new Rectangle( 1, 2, 3, 4 );
			var resolutions = A.CollectionOfFake<DisplayResolution>( 5 );
			var display = new DisplayCapabilities( true, bounds, 60, 24, resolutions );
			
			Assert.IsTrue( display.IsPrimary );
			Assert.AreEqual( bounds, display.Bounds );
			Assert.AreEqual( 60, display.RefreshRate );
			Assert.IsTrue( resolutions.SequenceEqual( display.Resolutions ) );
		}
	}
}

