using System;
using Life.Math;
using OpenTK;
using OpenTK.Platform;
using Life;

namespace Life.Platform
{
	public class RenderWindowService : IService
    {
        #region [ Private Members ]

        private readonly IGameWindow window;

        #endregion [ Private Members ]

        #region [ Public Properties ]

        public delegate void OnCloseDelegate( RenderWindowService window );
        public delegate void OnResizeDelegate( int width, int height );

        public event OnCloseDelegate OnClose;
        public event OnResizeDelegate OnResize;

        public int Width
        {
            get { return this.window.Width; }
        }

        public int Height
        {
            get { return this.window.Height; }
        }
       
       	public IGameWindow Window
		{
			get { return this.window; }
		}
		
        #endregion [ Public Properties ]

        public RenderWindowService( IGameWindow window )
        {
        	this.window = window;
        	this.window.Visible = true;
        }

        #region [ IService Implementation ]
        
		public string Name
		{
			get { return @"Render Window Service"; }
		}

        public void Start( Kernel kernel )
        {
            // Setup the events to handle
            this.window.Closing += ( sender, e ) =>
            {
                var closing = this.OnClose;
                if( closing != null )
                    closing( this );

                this.Status = ServiceStatus.Dead;
            };

            this.window.Resize += ( sender, e ) => {
				var handler = this.OnResize;
				
				if ( handler != null )
					handler( this.window.Width, this.window.Height );
			};
			
            this.Status = ServiceStatus.Alive;
        }

        public void Stop( Kernel kernel )
        {
            this.window.Close( );
        }

        /// <summary>
        /// Handles the message loop
        /// </summary>
        /// <param name="gameTime">Unused</param>
        public void Update( GameTime gameTime )
        {
           	this.window.ProcessEvents( ); 
        }
        
        /// <summary>
        /// Tells whether the service is still running
        /// </summary>
		public ServiceStatus Status { get; private set; }

        /// <summary>
        /// Priority of the service in the kernel
        /// </summary>
        public uint Priority
        {
            get { return uint.MaxValue; }
        }

        #endregion [ IService Implementation ]
        
		#region [ IDisposable Implementation ]
		
		~RenderWindowService( )
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
			if ( disposing )
				window.Dispose( );
		}
		
		#endregion
    }
		
}

