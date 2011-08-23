using System;
using Life.Core;

namespace Life.Graphics
{
	/// <summary>
	/// OpenGL buffer with ability to be stored on the GPU
	/// </summary>
	public abstract class HardwareBuffer : ResourceBase
	{
		protected HardwareBuffer ()
		{
		}

		#region implemented abstract members of Life.Core.ResourceBase
		
		protected override void Dispose( bool disposing )
		{
			if ( disposing && State == ResourceState.Loaded )
				Unload( );
		}

		#endregion
	}
}

