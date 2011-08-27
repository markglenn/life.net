using System;
using Life;
using Life.Platform;
using OpenTK;

namespace SpinningCube
{
	public class Example
	{
		public static void Main( string[ ] args )
		{ 
			var kernel = new Kernel( );
			
			var window = new GameWindow( 400, 300 );
			var windowService = new RenderWindowService( window );
			
			kernel.AddService( windowService );
		}
	}
}

