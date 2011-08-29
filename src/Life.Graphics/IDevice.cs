using System;
using System.Collections.Generic;
using Life;
using Life.Platform;
using Life.Math;

namespace Life.Graphics
{
    public enum MatrixType
    {
        Projection,
        ModelView,
        View
    }
 
	public interface IDevice : IService
	{
		/// <summary>
		/// Gets a list of attached adapters
		/// </summary>
		IEnumerable<AdapterCapabilities> GetAdapters( );
		
		/// <summary>
		/// Gets the window service.
		/// </summary>
		RenderWindowService WindowService { get; }
		
		void Render( RenderOperation operation );

        /// <summary>
        /// Sets a matrix within the device
        /// </summary>
        /// <param name="matrixType">Type of matrix being set</param>
        /// <param name="matrix">Matrix to set</param>
        void SetMatrix( MatrixType matrixType, Matrix4 matrix );

		HardwareVertexBuffer CreateVertexBuffer( VertexDefinition vertexDefinition, int numVertices );
		
		HardwareIndexBuffer CreateIndexBuffer( IndexBufferFormat format, int numIndices );
		
	}
}

