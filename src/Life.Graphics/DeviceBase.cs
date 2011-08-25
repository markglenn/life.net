using System;
using System.Collections.Generic;
using Life.Platform;
using Life;

namespace Life.Graphics
{
	public abstract class DeviceBase : IDevice
	{
		#region [ Private Members ]
		
		private readonly RenderWindowService windowService;
		
		#endregion
		
		protected DeviceBase( RenderWindowService window )
		{
			this.windowService = window;
		}

		#region [ IDevice implementation ]
		
		public abstract IEnumerable<AdapterCapabilities> GetAdapters( );

		public RenderWindowService WindowService 
		{
			get { return this.windowService; }
		}
		
		#endregion

		#region [ IService implementation ]
		
		public abstract void Start( );

		public abstract void Stop( );

		public abstract void Update( GameTime gameTime );

		public abstract string Name { get; }

		public virtual ServiceStatus Status { get; protected set; }

		public virtual uint Priority { get; protected set; }
		
		#endregion

		#region [ IDisposable implementation ]
		
		~DeviceBase( )
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
		
		}
		
		#endregion
	}
}

