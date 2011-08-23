using System;

namespace Life.Graphics
{
	public abstract class HardwareVertexBuffer : HardwareBuffer
	{
        #region [ Private Members ]

        private readonly VertexDefinition vertexDefinition;
        private readonly uint numVertices;

        #endregion [ Private Members ]

        #region [ Public Properties ]

        public uint NumVertices
        {
            get { return this.numVertices; }
        }

        public uint Stride
        {
            get { return this.vertexDefinition.Stride; }
        }

        public VertexDefinition VertexDefinition
        {
            get { return this.vertexDefinition; }
        }

        #endregion [ Public Properties ]

        protected HardwareVertexBuffer( VertexDefinition vertexDefinition, uint numVertices )
        {
            this.vertexDefinition = vertexDefinition;
            this.numVertices = numVertices;
        }
	}
}

