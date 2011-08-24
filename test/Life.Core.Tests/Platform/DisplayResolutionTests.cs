using System;
using NUnit.Framework;
using FakeItEasy;
using System.Drawing;

namespace Life.Core.Platform.Tests
{
	[TestFixture]
	public class DisplayResolutionTests
	{
		[Test]
		public void Ctor_SetsBounds( )
		{
			var size = new Size( 100, 200 );
			var resolution = new DisplayResolution( size, 24, 60 );
			
			Assert.AreEqual( size, resolution.Bounds );
		}
		
		[Test]
		public void Ctor_SetsBPP( )
		{
			Assert.AreEqual( 24, new DisplayResolution( A.Dummy<Size>( ), 24, 50 ).BitsPerPixel );
		}
		
		[Test]
		public void Ctor_SetsRefresh( )
		{
			Assert.AreEqual( 65, new DisplayResolution( A.Dummy<Size>( ), 24, 65 ).RefreshRate );
		}
	}
}

