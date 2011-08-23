using System;

namespace Life.Graphics
{
	public sealed class BufferLockScope : IDisposable
	{
		private readonly Action unlock;
		
		public BufferLockScope( Action unlock )
		{
			this.unlock = unlock;
		}
		
		public void Dispose( )
		{
			this.unlock( );
		}
	}
}

