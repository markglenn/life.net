using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Life.Input
{
    public abstract class Keyboard : IService, IDisposable
    {
        protected Keyboard( )
        {
        }

        /// <summary>
        /// Is the key down
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if the state of the keyboard is down</returns>
        public abstract bool IsKeyDown( KeyCode key );

        /// <summary>
        /// Captures the keyboard state
        /// </summary>
        /// <returns>True if captured successfully</returns>
        public abstract bool Capture( );

        #region [ IService Members ]

        /// <summary>
        /// Startup for the service
        /// </summary>
        /// <param name="kernel"></param>
        /// <returns>True if properly started</returns>
        public abstract void Start( Kernel kernel );

        /// <summary>
        /// Stops a service
        /// </summary>
        public abstract void Stop( Kernel kernel );

        /// <summary>
        /// Relative priority of the service (0 is highest)
        /// </summary>
        public uint Priority
        {
            get { return 100; }
        }

		public abstract string Name { get; }

		public ServiceStatus Status { get; protected set; }
		
        /// <summary>
        /// Called every frame
        /// </summary>
        /// <param name="gameTime">In game time</param>
        public void Update( GameTime gameTime )
        {
            this.Capture( );
        }

        #endregion [ IService Members ]

        #region [ Implementation of IDisposable ]

        ~Keyboard( )
        {
            Dispose( false );
        }

        public void Dispose( )
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected abstract void Dispose( bool disposing );

        #endregion [ Implementation of IDisposable ]
    }
}


