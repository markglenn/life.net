using System;
using Life;

namespace Life.Graphics
{
	public enum BufferLock
	{
		ReadWrite,
		ReadOnly,
		Discard
	}
	
	public enum BufferUsage
	{
		Static,
		Dynamic
	}
	
	/// <summary>
	/// OpenGL buffer with ability to be stored on the GPU
	/// </summary>
	public abstract class HardwareBuffer : ResourceBase
	{
		#region [ Private Members ]
		
		private bool isLocked;
		
		#endregion
		
		#region [ Public Properties ]
		
		public bool IsLocked
		{
			get { return this.isLocked; }
		}
		
		#endregion
		
		protected HardwareBuffer( )
		{
		}
		
		public BufferLockScope Lock( BufferLock lockType )
		{
			if ( this.isLocked )
				return null;
			
			var bufferLock = new BufferLockScope( this.Unlock );
			this.isLocked = DoLock( lockType );
			
			return bufferLock;
		}
		
		public void Unlock( )
		{
			this.isLocked = false;
			DoUnlock( );
		}

		protected abstract bool DoLock( BufferLock lockType );
		protected abstract void DoUnlock( );
		
		public abstract void Write<T>( T val ) where T : struct;
		public abstract void Write<T>( T[ ] vals ) where T : struct;
		
		#region implemented abstract members of Life.ResourceBase
		
		protected override void Dispose( bool disposing )
		{
			if ( disposing && State == ResourceState.Loaded )
				Unload( );
		}

		#endregion
	}
}

