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
using Quaternion = Life.Math.Quaternion;
using Matrix4 = Life.Math.Matrix4;
using System.Diagnostics;

namespace ColoredTriangle
{
    [StructLayout( LayoutKind.Sequential, Pack = 1 )]
    struct TriangleVertex
    {
        public readonly Vector3 position;
        public readonly uint color;

        public TriangleVertex( float x, float y, float z, Color color )
        {
            this.color = color.ToRgba( );
            this.position = new Vector3( x, y, z );
        }
        
		public static VertexDefinition VertexDefinition
		{
			get 
			{
				return new VertexDefinition( new[ ] {
	                new VertexElement( VertexElementType.Position, VertexElementFormat.Float, 3 ),
	                new VertexElement( VertexElementType.Color, VertexElementFormat.UByte, 4 )
	            } );
			}
		}
    }
    
	public class Example : IService, IRenderable
	{
        private HardwareIndexBuffer indexBuffer;
        private HardwareVertexBuffer vertexBuffer;
        private readonly IDevice device;
 
		public static void Main( string[ ] args )
		{
			TextWriterTraceListener myWriter = new TextWriterTraceListener(System.Console.Out);
			Trace.Listeners.Add( myWriter );
			 
			var kernel = new Kernel( );
			
			var window = new OpenTK.GameWindow( 400, 300, 
				new GraphicsMode( new ColorFormat( 8, 8, 8, 8 ), 16 ), 
				"OpenGL 3.1 Example", 0,
	            DisplayDevice.Default, 2, 0, // use the default display device, request a 3.1 OpenGL context
	            GraphicsContextFlags.Debug 
			);
			var windowService = new RenderWindowService( window );
			var device = new OpenGLDevice( windowService );
			var example = new Example( device );
			
			kernel.AddService( windowService );
			kernel.AddService( device );
			kernel.AddService( example );
			
			foreach( var service in kernel )
				Console.WriteLine( "{0} - {1}", service.Priority, service.Name );
				
			kernel.Run( );
			
			example.Dispose( );
			device.Dispose( );
			windowService.Dispose( );
		}
		
		public Example( IDevice device )
		{
			this.device = device;
			
			this.device.WindowService.OnClose += (window) => {
				this.Status = ServiceStatus.Dead;
			};
		}

		#region [ IRenderable implementation ]
		
		public RenderOperation RenderOperation { get; private set; }

		public Quaternion WorldOrientation { get; set; }

		public Vector3 WorldPosition { get; set; }

		public IEnumerable<Matrix4> Matrices { get; set; }
		
		#endregion

		#region [ IService implementation ]
		
		public void Start( Kernel kernel )
		{
            this.vertexBuffer = device.CreateVertexBuffer( TriangleVertex.VertexDefinition, 3 );
            this.indexBuffer = device.CreateIndexBuffer( IndexBufferFormat.UShort, 3 );

            using ( vertexBuffer.Lock( BufferLock.Discard ) )
            {
            	vertexBuffer.Write( new TriangleVertex( -1, 0, 0, Color.Red ) );
            	vertexBuffer.Write( new TriangleVertex( 0, 1, 0, Color.Green ) );
            	vertexBuffer.Write( new TriangleVertex( 0, 0, 0, Color.Blue ) );
            }

            using ( indexBuffer.Lock( BufferLock.Discard ) )
            {
                indexBuffer.Write( new ushort[] { 0, 1, 2 } );
            }

            this.RenderOperation = new RenderOperation( OperationType.TriangleList, 3, this.vertexBuffer, this.indexBuffer );
            this.Status = ServiceStatus.Alive;
		}

		public void Stop( Kernel kernel = null )
		{
			this.Status = ServiceStatus.Dead;
		}

		public void Update( GameTime gameTime )
		{
			this.device.Render( this.RenderOperation );
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

