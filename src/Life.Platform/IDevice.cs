using System;
using System.Collections.Generic;
using Life;

namespace Life.Platform
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
		
	}
}

