using System;

namespace Life.Graphics
{
   public enum OperationType
    {
        PointList,
        LineList,
        LineStrip,
        TriangleList,
        TriangleStrip,
        TriangleFan
    };

    public class RenderOperation
    {
        #region [ Public Properties ]

        /// <summary>
        /// Vertex buffer used to render
        /// </summary>
        public HardwareVertexBuffer VertexBuffer { get; set; }

        /// <summary>
        /// Index buffer used to index into the vertex buffer
        /// </summary>
        public HardwareIndexBuffer IndexBuffer { get; set; }

        /// <summary>
        /// Type to render
        /// </summary>
        public OperationType OperationType { get; set; }

        /// <summary>
        /// Number of primitives in the buffers
        /// </summary>
        public int PrimitiveCount { get; set; }

        #endregion [ Public Properties ]

        /// <summary>
        /// Creates a render operation using an index buffer
        /// </summary>
        /// <param name="operationType">Type to render</param>
        /// <param name="primitiveCount">Number of primitives</param>
        /// <param name="vertexBuffer">Vertex buffer</param>
        /// <param name="indexBuffer">Index buffer</param>
        public RenderOperation( OperationType operationType, int primitiveCount, 
            HardwareVertexBuffer vertexBuffer, HardwareIndexBuffer indexBuffer )
            : this( operationType, primitiveCount, vertexBuffer )
        {
            if( indexBuffer == null )
                throw new ArgumentNullException( "indexBuffer" );

            this.IndexBuffer = indexBuffer;
        }

        /// <summary>
        /// Creates a render operation without an index buffer
        /// </summary>
        /// <param name="operationType">Type to render</param>
        /// <param name="primitiveCount">Number of primitives</param>
        /// <param name="vertexBuffer">Vertex buffer</param>
        public RenderOperation( OperationType operationType, int primitiveCount, 
            HardwareVertexBuffer vertexBuffer )
        {
            if( vertexBuffer == null )
                throw new ArgumentNullException( "vertexBuffer" );

            if ( primitiveCount <= 0 )
                throw new InvalidOperationException( "Primitive count must be greater than zero" );

            this.VertexBuffer = vertexBuffer;
            this.OperationType = operationType;
            this.PrimitiveCount = primitiveCount;
        }
    }

}

