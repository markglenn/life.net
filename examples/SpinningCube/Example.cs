using System;
using Life;
using Life.Platform;
using Life.Graphics.OpenGL;
using System.Runtime.InteropServices;
using System.Drawing;
using Life.Graphics;
using Life.Core;
using Life.Math;
using OpenTK.Graphics;
using ExampleCore;

namespace SpinningCube
{
 	public class Example : IService
	{
		#region [ Private Members ]
		
        private HardwareIndexBuffer indexBuffer;
        private HardwareVertexBuffer vertexBuffer;
        private readonly IDevice device;
        private RenderOperation renderOperation;
        private Camera camera;
        
		#endregion
 
		public static void Main( string[ ] args )
		{
			var kernel = new Kernel( );
			
			var window = new OpenTK.GameWindow( 400, 300, 
				new GraphicsMode( new ColorFormat( 8, 8, 8, 8 ), 16 ), 
				"OpenGL 3.1 Example", 0,
	            OpenTK.DisplayDevice.Default, 2, 0, // use the default display device, request a 3.1 OpenGL context
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
			
			this.camera = new Camera( GetProjection( 400, 300 ), Vector3.Zero,
				Quaternion.FromAxisAngle( Vector3.UnitY, 0 ) );
				
			this.device.SetMatrix( MatrixType.Projection, this.camera.Projection );
			this.device.WindowService.OnClose += (window) => {
				this.Status = ServiceStatus.Dead;
			};
			this.device.WindowService.OnResize += (width, height) => {
				this.camera.Projection = GetProjection( width, height );	
			};
		}

		#region [ IService implementation ]
		
		public void Start( Kernel kernel )
		{
            this.vertexBuffer = device.CreateVertexBuffer( TexturedVertex.VertexDefinition, 3 );
            this.indexBuffer = device.CreateIndexBuffer( IndexBufferFormat.UShort, 3 );

            using ( vertexBuffer.Lock( BufferLock.Discard ) )
            {
            	vertexBuffer.Write( new TexturedVertex( -0.75f, -0.75f, 0, 0, 1 ) );
            	vertexBuffer.Write( new TexturedVertex( 0f, 0.75f, 0, 0.5f, 0 ) );
            	vertexBuffer.Write( new TexturedVertex( 0.75f, -0.75f, 0, 1, 1 ) );
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
			var rotation = Quaternion.FromAxisAngle( Vector3.UnitY, 
				( float )( gameTime.TotalTime.TotalSeconds ) * 2.0f );
			this.device.SetMatrix( MatrixType.ModelView, 
				camera.View * Matrix4.Translation( 0, 0, 2 ) * rotation.ToMatrix4( ) );
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
		
		private static Matrix4 GetProjection( int width, int height )
        {
            return Matrix4.Projection( ( float )System.Math.PI / 3,
                ( float )width / height, 0.001f, 1000.0f );
        }
        
		#region [ IDisposable implementation ]
		
		public void Dispose ()
		{
			this.vertexBuffer.Dispose( );
			this.indexBuffer.Dispose( );
		}
		
		#endregion
	}
}

