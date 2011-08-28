using System;
using Life;
using Life.Platform;
using OpenTK;
using Life.Graphics.OpenGL;
using System.Runtime.InteropServices;
using System.Drawing;

namespace SpinningCube
{
    [StructLayout( LayoutKind.Sequential, Pack = 1 )]
    struct CubeVertex
    {
        public readonly int color;
        public readonly Vector3 position;

        public CubeVertex( float x, float y, float z, Color color )
        {
            this.color = color.ToArgb( );
            this.position = new Vector3( x, y, z );
        }
    }
    
	public class Example
	{
		public static void Main( string[ ] args )
		{ 
			var kernel = new Kernel( );
			
			var window = new GameWindow( 400, 300 );
			var windowService = new RenderWindowService( window );
			var device = new OpenGLDevice( windowService );
			
			kernel.AddService( windowService );
			kernel.AddService( device );
			
			kernel.Run( );
		}
	}
}

