using System;
using OpenTK.Graphics.OpenGL;

namespace Life.Graphics.OpenGL
{
	public class OpenGLVertexBuffer : HardwareVertexBuffer
	{
		#region [ Private Members ]
		
		private uint vboId;
		
		#endregion
		
		public uint BufferID
		{
			get { return this.vboId; }
		}
		
		public OpenGLVertexBuffer( VertexDefinition vertexDefinition, uint numVertices )
			: base( vertexDefinition, numVertices )
		{
			GL.GenBuffers( 1, out vboId );
		}
		
		protected override void Dispose( bool disposing )
		{
			if ( disposing )
				GL.DeleteBuffers( 1, ref vboId );
				
			base.Dispose( disposing );
		}
		
		protected override bool DoLock (BufferLock lockType)
		{
			throw new NotImplementedException ();
		}
		
		protected override void DoUnlock ()
		{
			throw new NotImplementedException ();
		}
	}
}

