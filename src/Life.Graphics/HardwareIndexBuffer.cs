using System;

namespace Life.Graphics
{
	public enum IndexBufferFormat
	{
		Byte,
		Short,
		Int,
		UByte,
		UShort, 
		UInt
	};
	
	public abstract class HardwareIndexBuffer : HardwareBuffer
	{
		#region [ Private Members ]
		
		private readonly IndexBufferFormat format;
		private readonly int numIndices;
		
		#endregion
		
		#region [ Public Properties ]
		
		public IndexBufferFormat Format
		{
			get { return this.format; }
		}
		
		public int NumIndices
		{
			get { return this.numIndices; }
		}
		
		#endregion
		
		protected HardwareIndexBuffer( IndexBufferFormat format, int numIndices )
		{
			this.format = format;
			this.numIndices = numIndices;
		}
	}

}

