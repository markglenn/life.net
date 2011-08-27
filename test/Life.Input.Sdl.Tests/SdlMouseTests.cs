using System;
using NUnit.Framework;
using FakeItEasy;

namespace Life.Input.Sdl.Tests
{
	[TestFixture]
	public class SdlMouseTests
	{
		[Test]
		public void Start_InitializesSdlMouse( )
		{
			var mouse = new SdlMouse( );
			mouse.Start( A.Dummy<Kernel>( ) );
			
			Assert.AreEqual( ServiceStatus.Alive, mouse.Status );
		}
	}
}

