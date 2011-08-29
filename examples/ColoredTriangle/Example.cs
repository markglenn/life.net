using System;
using Life;
using Life.Platform;
using Life.Graphics.OpenGL;
using System.Runtime.InteropServices;
using System.Drawing;
using Life.Math;
using Life.Graphics;
using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK;

using Vector3 = Life.Math.Vector3;
using ExampleCore;

namespace ColoredTriangle
{
	public class Example : IService
	{
		#region [ Private Members ]
		
        private HardwareIndexBuffer indexBuffer;
        private HardwareVertexBuffer vertexBuffer;
        private readonly IDevice device;
        private RenderOperation renderOperation;
        
		#endregion
 
		public static void Main( string[ ] args )
		{
			var kernel = new Kernel( );
			
			var window = new OpenTK.GameWindow( 400, 300, 
				new GraphicsMode( new ColorFormat( 8, 8, 8, 8 ), 16 ), 
				"OpenGL 3.1 Example", 0,
	            DisplayDevice.Default, 2, 0, // use the default display device, request a 3.1 OpenGL context
	            GraphicsContextFlags.Debug 
			);
			
			using( var windowService = new RenderWindowService( window ) )
			using( var device = new OpenGLDevice( windowService ) )
			using( var example = new Example( device ) )
			{
				kernel.AddService( windowService );
				kernel.AddService( device );
				kernel.AddService( example );
				
				foreach( var service in kernel )
					Console.WriteLine( "{0} - {1}", service.Priority, service.Name );
					
				kernel.Run( );
			}
		}
		
		public Example( IDevice device )
		{
			this.device = device;
			
			this.device.WindowService.OnClose += (window) => {
				this.Status = ServiceStatus.Dead;
			};
		}

		#region [ IService implementation ]
		
		public void Start( Kernel kernel )
		{
            this.vertexBuffer = device.CreateVertexBuffer( ColoredVertex.VertexDefinition, 3 );
            this.indexBuffer = device.CreateIndexBuffer( IndexBufferFormat.UShort, 3 );

            using ( vertexBuffer.Lock( BufferLock.Discard ) )
            {
            	vertexBuffer.Write( new ColoredVertex( -0.75f, -0.75f, 0, Color.Red ) );
            	vertexBuffer.Write( new ColoredVertex( 0f, 0.75f, 0, Color.Green ) );
            	vertexBuffer.Write( new ColoredVertex( 0.75f, -0.75f, 0, Color.Blue ) );
            }

            using ( indexBuffer.Lock( BufferLock.Discard ) )
                indexBuffer.Write( new ushort[] { 0, 1, 2 } );

            this.renderOperation = new RenderOperation( OperationType.TriangleList, 3, this.vertexBuffer, this.indexBuffer );
            this.Status = ServiceStatus.Alive;
		}

		public void Stop( Kernel kernel = null )
		{
			this.Status = ServiceStatus.Dead;
		}

		public void Update( GameTime gameTime )
		{
			this.device.Render( this.renderOperation );
		}

		public string Name 
		{
			get { return "Triangle Example Service"; }
		}

		public ServiceStatus Status 
		{
			get; private set;
		}

		public uint Priority 
		{ 
			get { return 200; } 
		}
		
		#endregion

		#region [ IDisposable implementation ]
		
		public void Dispose ()
		{
			this.vertexBuffer.Dispose( );
			this.indexBuffer.Dispose( );
		}
		
		#endregion
	}
}