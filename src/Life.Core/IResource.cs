using System;

namespace Life.Core
{
	public enum ResourceState
	{
		Unloaded,
		Loading,
		Loaded,
		Unloading
	}
	
	public interface IResource : IDisposable
	{
		ResourceState State { get; }
		
		bool Load( );
		bool Unload( );
	}
}

