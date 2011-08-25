using System;
using OpenTK.Graphics.OpenGL;

namespace Life.Graphics.OpenGL
{
	public class OpenGLIndexBuffer : HardwareIndexBuffer
	{
		#region [ Private Members ]

        private int iboId;
        private int offset;
        private readonly int bufferSize;
        private readonly int indexSize;
        private BufferUsageHint usage;

        #endregion [ Private Members ]

        public OpenGLIndexBuffer( IndexBufferFormat format, BufferUsage usage, int numIndices ) 
            : base( format, numIndices )
        {
			this.indexSize = GetSizeFromFormat( format );	
            this.bufferSize = numIndices * this.indexSize;
            GL.GenBuffers( 1, out iboId );

            this.usage = ( usage == BufferUsage.Static ) ? 
                BufferUsageHint.StreamDraw : BufferUsageHint.DynamicDraw;
        }

        public int BufferId
        {
            get { return this.iboId; }
        }

        private static int GetSizeFromFormat( IndexBufferFormat format )
        {
            switch( format )
            {
                case IndexBufferFormat.Byte:
                case IndexBufferFormat.UByte:
                    return 1;

                case IndexBufferFormat.Short:
                case IndexBufferFormat.UShort:
                    return 2;

                case IndexBufferFormat.Int:
                case IndexBufferFormat.UInt:
                    return 4;
            }

            throw new InvalidOperationException( "Could not set index format to " + format );
        }

        protected override bool DoLock( BufferLock lockType )
        {
            if ( lockType != BufferLock.Discard )
                throw new NotSupportedException( "Only discard locks are supported" );

            GL.BindBuffer( BufferTarget.ElementArrayBuffer, iboId );
            GL.BufferData( BufferTarget.ElementArrayBuffer,
                ( IntPtr )( NumIndices * GetSizeFromFormat( this.Format ) ), IntPtr.Zero,
                this.usage );

            this.offset = 0;

            return GL.GetError( ) == 0;
        }

        protected override void DoUnlock( )
        {
            GL.BindBuffer( BufferTarget.ArrayBuffer, 0 );
        }

        public override void Write<T>( T value )
        {
            Write( new[ ] { value } );
        }

        public override void Write<T>( T[ ] values )
        {
            // Calculate size of the write
            int size = values.Length * this.indexSize;

            // Check bounds
            if ( size + this.offset > this.bufferSize )
                throw new InvalidOperationException( "Tried to write too much data to index buffer." );

            GL.BufferSubData( BufferTarget.ElementArrayBuffer,
                new IntPtr( this.offset ), new IntPtr( size ), values );

            this.offset += size;
        }

        protected override void Dispose( bool disposing )
        {
            GL.DeleteBuffers( 1, ref this.iboId );

            base.Dispose( disposing );
        }

	}
}

