using System;
using System.Linq;
using NUnit.Framework;
using FakeItEasy;
using Life.Platform;

namespace Life.Graphics.OpenGL.Tests
{
	[TestFixture]
	public class OpenGLDeviceTests
	{
		[Test]
		public void GetAdapters_ReturnDisplayAdapterInformation( )
		{
			var adapters = new OpenGLDevice( A.Dummy<RenderWindowService>( ) ).GetAdapters( );
			
			Assert.AreNotEqual( 0, adapters.Count( ) );
			Assert.AreNotEqual( 0, adapters.First( ).Displays.Count( ) );
		}

	}
}

