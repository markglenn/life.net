using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace Life.Graphics.OpenGL
{
	public class OpenGLVertexBuffer : HardwareVertexBuffer
	{
		#region [ Private Members ]
		
		private uint vboId;
		private int offset;
		
		private readonly BufferUsageHint usage;
		private readonly int bufferSize;
		
        private static readonly IDictionary<VertexElementFormat, TexCoordPointerType> TextureCoordinateType =
            new Dictionary<VertexElementFormat, TexCoordPointerType> {
                { VertexElementFormat.Float, TexCoordPointerType.Float },
                { VertexElementFormat.Double, TexCoordPointerType.Double },
                { VertexElementFormat.Short, TexCoordPointerType.Short },
                { VertexElementFormat.Int, TexCoordPointerType.Int },
            };

        private static readonly IDictionary<VertexElementFormat, VertexPointerType> VertexType =
            new Dictionary<VertexElementFormat, VertexPointerType> {
                { VertexElementFormat.Float, VertexPointerType.Float },
                { VertexElementFormat.Double, VertexPointerType.Double },
                { VertexElementFormat.Short, VertexPointerType.Short },
                { VertexElementFormat.Int, VertexPointerType.Int },
            };

        private static readonly IDictionary<VertexElementFormat, NormalPointerType> NormalType =
            new Dictionary<VertexElementFormat, NormalPointerType> {
                { VertexElementFormat.Float, NormalPointerType.Float },
                { VertexElementFormat.Double, NormalPointerType.Double },
                { VertexElementFormat.Short, NormalPointerType.Short },
                { VertexElementFormat.Int, NormalPointerType.Int },
                { VertexElementFormat.UByte, NormalPointerType.Byte }
            };

        private static readonly IDictionary<VertexElementFormat, ColorPointerType> ColorType =
            new Dictionary<VertexElementFormat, ColorPointerType> {
                { VertexElementFormat.Float, ColorPointerType.Float },
                { VertexElementFormat.Double, ColorPointerType.Double },
                { VertexElementFormat.Short, ColorPointerType.Short },
                { VertexElementFormat.Int, ColorPointerType.Int },
                { VertexElementFormat.UByte, ColorPointerType.UnsignedByte }
            };

        private static readonly IDictionary<VertexElementType, TextureUnit> TextureUnitType =
            new Dictionary<VertexElementType, TextureUnit> {
                { VertexElementType.Texture0, TextureUnit.Texture0 },
                { VertexElementType.Texture1, TextureUnit.Texture1 },
                { VertexElementType.Texture2, TextureUnit.Texture2 },
                { VertexElementType.Texture3, TextureUnit.Texture3 },
                { VertexElementType.Texture4, TextureUnit.Texture4 },
                { VertexElementType.Texture5, TextureUnit.Texture5 },
                { VertexElementType.Texture6, TextureUnit.Texture6 },
                { VertexElementType.Texture7, TextureUnit.Texture7 },
            };

		#endregion
		
		public uint BufferId
		{
			get { return this.vboId; }
		}
		
		public int BufferSize
		{
			get { return this.bufferSize; }
		}
		
		public OpenGLVertexBuffer( VertexDefinition vertexDefinition, BufferUsage usage, int numVertices )
			: base( vertexDefinition, numVertices )
		{
			GL.GenBuffers( 1, out vboId );
			
			this.usage = ( usage == BufferUsage.Static ) ? 
				BufferUsageHint.StaticDraw : BufferUsageHint.DynamicDraw;
			this.bufferSize = vertexDefinition.Stride * numVertices;
		}
	
        /// <summary>
        /// Enables the vertex definition
        /// </summary>
        public void EnableVertexDefinition( )
        {
            foreach ( var definition in this.VertexDefinition.Elements )
            {
                switch ( definition.Type )
                {
                    case VertexElementType.Color:
                    	GL.EnableClientState( ArrayCap.ColorArray );
                        GL.ColorPointer( definition.Count,
                            ColorType[ definition.Format ], 
                            this.VertexDefinition.Stride, definition.Offset );
                        break;

                    case VertexElementType.Normal:
                    	GL.EnableClientState( ArrayCap.NormalArray );
                        GL.NormalPointer( NormalType[ definition.Format ], this.VertexDefinition.Stride,
                                          definition.Offset );
                        break;

                    case VertexElementType.Position:
                    	GL.EnableClientState( ArrayCap.VertexArray );
                        GL.VertexPointer( definition.Count,
                            VertexType[ definition.Format ], this.Stride, definition.Offset );
                        break;

                    case VertexElementType.Texture0:
                    case VertexElementType.Texture1:
                    case VertexElementType.Texture2:
                    case VertexElementType.Texture3:
                    case VertexElementType.Texture4:
                    case VertexElementType.Texture5:
                    case VertexElementType.Texture6:
                    case VertexElementType.Texture7:
                    	GL.EnableClientState( ArrayCap.TextureCoordArray );
                        GL.ClientActiveTexture( TextureUnitType[ definition.Type ] );
                        GL.TexCoordPointer( definition.Count,
                            TextureCoordinateType[ definition.Format ], this.VertexDefinition.Stride,
                                            definition.Offset );
                        break;
                }

            }
        }	
        
		protected override void Dispose( bool disposing )
		{
			if ( disposing )
				GL.DeleteBuffers( 1, ref vboId );
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
		
		public override void Write<T>( T value )
		{
			Write( new[ ]{ value } );
		}
		
		public override void Write<T>( T[ ] values )
		{
            // Calculate size of the write
            int size = ( int )this.VertexDefinition.Stride * values.Length;

            // Check bounds
            if ( size + this.offset > this.bufferSize )
                throw new InvalidOperationException( "Tried to write too much data to vertex buffer." );

            GL.BufferSubData( BufferTarget.ArrayBuffer, 
               ( IntPtr )this.offset, ( IntPtr )size, values );

            this.offset += size;
		}
		
	}
}

