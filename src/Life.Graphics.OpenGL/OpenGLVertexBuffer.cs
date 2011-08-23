using System;
using OpenTK.Graphics.OpenGL;

namespace Life.Graphics.OpenGL
{
	public class OpenGLVertexBuffer : HardwareVertexBuffer
	{
		#region [ Private Members ]
		
		private uint vboId;
		private int offset;
		
		private readonly BufferUsageHint usage;
		private readonly uint bufferSize;
		
		#endregion
		
		public uint BufferID
		{
			get { return this.vboId; }
		}
		
		public uint BufferSize
		{
			get { return this.bufferSize; }
		}
		
		public OpenGLVertexBuffer( VertexDefinition vertexDefinition, BufferUsage usage, uint numVertices )
			: base( vertexDefinition, numVertices )
		{
			GL.GenBuffers( 1, out vboId );
			
			this.usage = ( usage == BufferUsage.Static ) ? 
				BufferUsageHint.StaticDraw : BufferUsageHint.DynamicDraw;
			this.bufferSize = vertexDefinition.Stride * numVertices;
		}
		
		protected override void Dispose( bool disposing )
		{
			if ( disposing )
				GL.DeleteBuffers( 1, ref vboId );
				
			base.Dispose( disposing );
		}
		
		protected override bool DoLock (BufferLock lockType)
		{
			if ( lockType != BufferLock.Discard )
				throw new NotSupportedException( );
				
			GL.BindBuffer( BufferTarget.ArrayBuffer, this.vboId );
			GL.BufferData( BufferTarget.ArrayBuffer, new IntPtr( this.bufferSize ),
				IntPtr.Zero, this.usage );
			
			this.offset = 0;
			
			return GL.GetError( ) == ErrorCode.NoError;
		}
		
		protected override void DoUnlock ()
		{
			GL.BindBuffer( BufferTarget.ArrayBuffer, 0 );
		}
	}
}

