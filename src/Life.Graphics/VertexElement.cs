using System;

namespace Life.Graphics
{
	public class VertexElement : IComparable<VertexElement>
	{
		#region [ Private Members ]
		
		private readonly int size;
		private readonly int count;
		private readonly VertexElementType type;
		private readonly VertexElementFormat format;
		
		#endregion
		
		#region [ Public Properties ]
		
		public int Count
		{
			get { return this.count; }
		}
		
		public int Size
		{
			get { return this.size; }
		}
		
		public VertexElementType Type
		{
			get { return this.type; }
		}
		
		public VertexElementFormat Format
		{
			get { return this.format; }
		}
		
		public int Offset 
		{
			get; internal set; 
		}
		
		#endregion
		
		public VertexElement( VertexElementType type, VertexElementFormat format, int count )
		{
			this.type = type;
			this.format = format;
			this.size = SizeOfElement( format, count );
			this.count = count;
		}

		#region [ IComparable[VertexElement] implementation ]
		
		public int CompareTo (VertexElement other)
		{
			return this.type.CompareTo( other.type );
		}
		
		#endregion
		
		private static int SizeOfElement( VertexElementFormat format, int count )
		{
			switch( format )
			{
			case VertexElementFormat.UByte:
				return count;
				
			case VertexElementFormat.Short:
				return count * 2;
				
			case VertexElementFormat.Int:
			case VertexElementFormat.Float:
				return count * 4;
				
			case VertexElementFormat.Double:
				return count * 8;
			}
			
			throw new InvalidOperationException( "Unknown vertex format " + format.ToString( ) );
		}
	}
}

