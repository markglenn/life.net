using System;
using System.Collections.Generic;
using Life;
using Life.Platform;

namespace Life.Graphics
{
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
		
		HardwareVertexBuffer CreateVertexBuffer( VertexDefinition vertexDefinition, int numVertices );
		
		HardwareIndexBuffer CreateIndexBuffer( IndexBufferFormat format, int numIndices );
		
	}
}

