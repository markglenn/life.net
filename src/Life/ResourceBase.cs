using System;

namespace Life
{
	public abstract class ResourceBase : IResource
	{
		public ResourceBase( string name )
		{
			this.Name = name;
		}
		
		#region [ IResource implementation ]
		
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
		
		public string Name { get; protected set; }
		
		~ResourceBase( )
		{
			Dispose( false );
		}
		
		public void Dispose( )
		{
			Dispose( true );
			GC.SuppressFinalize( this );
		}
		
		protected virtual void Dispose( bool disposing )
		{
			if ( disposing && this.State == ResourceState.Loaded )
				this.Unload( );
		}
		
		#endregion
		
		protected abstract bool DoLoad( );
		protected abstract bool DoUnload( );
	}
}

