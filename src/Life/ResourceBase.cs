using System;

namespace Life
{
	public abstract class ResourceBase : IResource
	{
		#region IResource implementation
		
		public bool Load( )
		{
			if( this.State != ResourceState.Unloaded )
				return false;
			
			this.State = ResourceState.Loading;
			
			this.State = DoLoad( ) ? ResourceState.Loaded : ResourceState.Unloaded;
			
			return this.State == ResourceState.Loaded;
		}

		public bool Unload( )
		{
			if( this.State != ResourceState.Loaded )
				return false;
			
			this.State = ResourceState.Loading;
			
			this.State = DoUnload( ) ? ResourceState.Unloaded : ResourceState.Loaded;
			
			return this.State == ResourceState.Unloaded;
		}

		public ResourceState State { get; private set; }
		
		~ResourceBase( )
		{
			Dispose( false );
		}
		
		public void Dispose( )
		{
			Dispose( true );
			GC.SuppressFinalize( this );
		}
		
		protected abstract void Dispose( bool disposing );
		
		#endregion
		
		public virtual bool DoLoad( ) { return true; }
		public virtual bool DoUnload( ) { return true; }
	}
}

