using System;
using System.Runtime.InteropServices;
using Life.Math;
using Life.Graphics;

namespace ExampleCore
{
    [StructLayout( LayoutKind.Sequential, Pack = 1 )]
	public struct TexturedVertex
	{
    	public readonly Vector3 position;
    	public readonly float tu, tv;
    	
		public TexturedVertex( float x, float y, float z, float tu, float tv )
        {
            this.position = new Vector3( x, y, z );
            this.tu = tu;
			this.tv = tv;
        }
        
		public static VertexDefinition VertexDefinition
		{
			get 
			{
				return new VertexDefinition( new[ ] {
	                new VertexElement( VertexElementType.Position, VertexElementFormat.Float, 3 ),
	                new VertexElement( VertexElementType.Texture0, VertexElementFormat.Float, 2 )
	            } );
			}
		}
	}
	
}

