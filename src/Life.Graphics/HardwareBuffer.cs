using System;
using OpenTK.Graphics.OpenGL;

namespace Life.Graphics
{
	/// <summary>
	/// OpenGL buffer with ability to be stored on the GPU
	/// </summary>
	public abstract class HardwareBuffer : IDisposable
	{
		#region [ Private Members ]
		
		private uint[] bufferID = new uint[ 1 ];
		
		#endregion
		
		/// <summary>
		/// OpenGL buffer ID
		/// </summary>
		public uint BufferID
		{
			get { return this.bufferID[0]; }
			protected set { this.bufferID[0] = value; }
		}
			
		protected HardwareBuffer ()
		{
			GL.GenBuffers( 1, this.bufferID );
		}

		~HardwareBuffer( )
		{
			Dispose( false );
		}
	
		#region [ IDisposable implementation ]
		
		public void Dispose ()
		{
			Dispose( true );		
			GC.SuppressFinalize( this );
		}
		
		protected virtual void Dispose( bool disposing )
		{
			GL.DeleteBuffers( 1, this.bufferID );
		}
		
		#endregion
	}
}

