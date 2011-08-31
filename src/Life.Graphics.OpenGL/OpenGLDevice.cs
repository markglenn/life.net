using System;
using System.Linq;
using Life.Platform;
using System.Collections.Generic;
using OpenTK;
using LifeDisplayResolution = Life.Platform.DisplayResolution;
using OpenTKDisplayResolution = OpenTK.DisplayResolution;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

using Vector3 = Life.Math.Vector3;
using Matrix4 = Life.Math.Matrix4;
using Life.Graphics.Materials;
using Life.Archive;
using Life.Graphics.OpenGL.Materials;

namespace Life.Graphics.OpenGL
{
	public class OpenGLDevice : DeviceBase
	{
		public OpenGLDevice( RenderWindowService window )
			: base( window )
		{
			window.OnClose += _ => {
				this.Status = ServiceStatus.Dead;
			};
			
			window.OnResize += ( width, height ) => {
				GL.Viewport( 0, 0, width, height );
			};
		}

		#region [ IDevice implementation ]
		
		public override IEnumerable<AdapterCapabilities> GetAdapters ()
		{
			return new[] { 
				new AdapterCapabilities( 
					DisplayDevice.AvailableDisplays.Select( d => GetDisplayCapabilities( d ) ) 
				) };
		}
		
		public override void Render( RenderOperation operation )
		{
			var vertexBuffer = (OpenGLVertexBuffer )operation.VertexBuffer;
			var indexBuffer = (OpenGLIndexBuffer )operation.IndexBuffer;
			
			if ( vertexBuffer == null )
				throw new ArgumentException( "Render operation requires vertex buffer" );
				
			GL.BindBuffer( BufferTarget.ArrayBuffer, vertexBuffer.BufferId );
			vertexBuffer.EnableVertexDefinition( );
			
			if ( indexBuffer != null )
			{
				GL.BindBuffer( BufferTarget.ElementArrayBuffer, indexBuffer.BufferId );
				GL.DrawElements( GetMode( operation.OperationType ), operation.PrimitiveCount,
					GetIndexBufferType( indexBuffer.Format ), 0 );
			}
			else
			{
				GL.DrawArrays( GetMode( operation.OperationType ), 0, operation.PrimitiveCount );
			}
		}
		
		public override HardwareVertexBuffer CreateVertexBuffer( VertexDefinition vertexDefinition, int numVertices )
		{
			return new OpenGLVertexBuffer( vertexDefinition, BufferUsage.Static, numVertices );
		}
		
		public override HardwareIndexBuffer CreateIndexBuffer( IndexBufferFormat format, int numIndices )
		{
			return new OpenGLIndexBuffer( format, BufferUsage.Static, numIndices );
		}
		
		public override Texture CreateTexture( IArchive archive, string name )
		{
			return new OpenGLTexture( name, archive );
		}
		
		public override void SetMatrix (MatrixType matrixType, Matrix4 matrix)
		{			
			switch( matrixType )
			{
			case MatrixType.ModelView:
				GL.MatrixMode( MatrixMode.Modelview );
				break;
				
			case MatrixType.Projection:
				GL.MatrixMode( MatrixMode.Projection );
				break;
			}
				
			GL.LoadMatrix( matrix.M );
		}
		#endregion

		#region [ IService implementation ]
		
		public override void Start( Kernel kernel )
		{
			GL.ClearColor( Color.White );
			GL.Viewport( 0, 0, this.WindowService.Width, this.WindowService.Height );

			GL.Clear( ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit );
			
			GL.Enable( EnableCap.DepthTest );
		}

		public override void Stop( Kernel kernel )
		{
		}

		public override void Update (GameTime gameTime)
		{
			this.WindowService.Window.SwapBuffers( );
			GL.Clear( ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit );
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
		
		private static BeginMode GetMode( OperationType operationType )
		{
			switch( operationType )
			{
			case OperationType.LineList:
				return BeginMode.Lines;
				
			case OperationType.LineStrip:
				return BeginMode.LineStrip;
				
			case OperationType.PointList:
				return BeginMode.Points;
				
			case OperationType.TriangleFan:
				return BeginMode.TriangleFan;
				
			case OperationType.TriangleList:
				return BeginMode.Triangles;
				
			case OperationType.TriangleStrip:
				return BeginMode.TriangleStrip;
				
			default:
				throw new InvalidOperationException( "Unknown mode" );
		
			}
		}
		
		private static DrawElementsType GetIndexBufferType( IndexBufferFormat format )
		{
			switch( format )
			{
			case IndexBufferFormat.UByte:
				return DrawElementsType.UnsignedByte;
				
			case IndexBufferFormat.UShort:
				return DrawElementsType.UnsignedShort;
				
			case IndexBufferFormat.UInt:
				return DrawElementsType.UnsignedInt;
				
			default:
				throw new InvalidOperationException( "Unknown index buffer type" );
				
			}
		}
		
		#endregion
	}
}

