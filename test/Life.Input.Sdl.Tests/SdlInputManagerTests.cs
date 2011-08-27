using System;
using NUnit.Framework;

namespace Life.Input.Sdl.Tests
{
	[TestFixture]
	public class SdlInputManagerTests
	{
		[Test]
		public void Ctor_InitializesSdl( )
		{
			// Really just testing to see that it doesn't crash
			using( var manager = new SdlInputManager( ) )
			{
			}
		}
		
		[Test]
		public void GetMouse_GetsSdlMouse( )
		{
			// Really just testing to see that it doesn't crash
			using( var manager = new SdlInputManager( ) )
			{
				using( var mouse = manager.GetMouse( ) )
					Assert.IsInstanceOf<SdlMouse>( mouse );
			}	
		}
	}
}

