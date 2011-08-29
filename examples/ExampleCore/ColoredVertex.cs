using System;
using Life.Math;
using Life.Graphics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ExampleCore
{
    [StructLayout( LayoutKind.Sequential, Pack = 1 )]
    public struct ColoredVertex
    {
        public readonly Vector3 position;
        public readonly uint color;

        public ColoredVertex( float x, float y, float z, Color color )
        {
            this.color = color.ToRgba( );
            this.position = new Vector3( x, y, z );
        }
        
		public static VertexDefinition VertexDefinition
		{
			get 
			{
				return new VertexDefinition( new[ ] {
	                new VertexElement( VertexElementType.Position, VertexElementFormat.Float, 3 ),
	                new VertexElement( VertexElementType.Color, VertexElementFormat.UByte, 4 )
	            } );
			}
		}
    }
}

