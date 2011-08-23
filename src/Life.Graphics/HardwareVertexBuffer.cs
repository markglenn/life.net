using System;

namespace Life.Graphics
{
	public abstract class HardwareVertexBuffer : HardwareBuffer
	{
        #region [ Private Members ]

        private readonly VertexDefinition vertexDefinition;
        private readonly int numVertices;

        #endregion [ Private Members ]

        #region [ Public Properties ]

        public int NumVertices
        {
            get { return this.numVertices; }
        }

        public int Stride
        {
            get { return this.vertexDefinition.Stride; }
        }

        public VertexDefinition VertexDefinition
        {
            get { return this.vertexDefinition; }
        }

        #endregion [ Public Properties ]

        protected HardwareVertexBuffer( VertexDefinition vertexDefinition, int numVertices )
        {
            this.vertexDefinition = vertexDefinition;
            this.numVertices = numVertices;
        }
	}
}

