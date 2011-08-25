using System;
using System.Linq;
using Life.Platform;
using System.Collections.Generic;
using OpenTK;
using LifeDisplayResolution = Life.Platform.DisplayResolution;
using OpenTKDisplayResolution = OpenTK.DisplayResolution;
using System.Drawing;

namespace Life.Graphics.OpenGL
{
	public class OpenGLDevice : DeviceBase
	{
		public OpenGLDevice( RenderWindowService window )
			: base( window )
		{
		}

		#region [ IDevice implementation ]
		
		public override IEnumerable<AdapterCapabilities> GetAdapters ()
		{
			return new[] { 
				new AdapterCapabilities( 
					DisplayDevice.AvailableDisplays.Select( d => GetDisplayCapabilities( d ) ) 
				) };
		}

		#endregion

		#region [ IService implementation ]
		
		public override void Start( Kernel kernel )
		{
		}

		public override void Stop( Kernel kernel )
		{
		}

		public override void Update (GameTime gameTime)
		{
			this.WindowService.Window.SwapBuffers( );
		}

		public override string Name 
		{
			get { return "OpenGL Device Service"; }
		}

		#endregion
		
		#region [ Private Methods ]
		
		private static LifeDisplayResolution GetResolution( OpenTKDisplayResolution resolution ) 
		{
			return new LifeDisplayResolution( new Size( resolution.Width, resolution.Height ),
				resolution.BitsPerPixel, ( int )resolution.RefreshRate );
		}
		
		private static DisplayCapabilities GetDisplayCapabilities( DisplayDevice display )
		{
			return new DisplayCapabilities( 
				display.IsPrimary, display.Bounds, ( int )display.RefreshRate, display.BitsPerPixel, 
				display.AvailableResolutions.Select( r => GetResolution( r ) ).ToArray( ) );
		}
		
		#endregion
	}
}

