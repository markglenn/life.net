using System;
using Life.Math;
using OpenTK;
using Matrix4 = Life.Math.Matrix4;

namespace Life.Core
{
	public class RenderWindow : IService
    {
        #region [ Private Members ]

        private readonly GameWindow window;

        #endregion [ Private Members ]

        #region [ Public Properties ]

        public delegate void OnCloseDelegate( RenderWindow window );
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
       
        #endregion [ Public Properties ]

        public RenderWindow( int width, int height )
        {
        	this.window = new GameWindow( width, height );
        }

        #region [ IService Implementation ]
        
		public string Name
		{
			get { return @"Render Window Service"; }
		}

        public void Start( )
        {
            // Setup the events to handle
            this.window.Closing += ( sender, e ) =>
            {
                var closing = this.OnClose;
                if( closing != null )
                    closing( this );

                this.Status = ServiceStatus.Dead;
            };

            this.window.Resize += ( sender, e ) => this.OnResize( this.window.Width, this.window.Height );
            this.Status = ServiceStatus.Alive;
        }

        public void Stop( )
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
		
		~RenderWindow( )
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
				window.Close( );
		}
		
		#endregion [ IDisposable Implementation ]
    }
		
}

