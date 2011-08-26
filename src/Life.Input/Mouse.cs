using System;
using System.Collections.Generic;
using System.Linq;
using Life.Math;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Life.Input
{
    public enum MouseButton : uint
    {
        Left = 0,
        Right,
        Middle
    }

    public abstract class Mouse : IService, IDisposable
    {
		#region [ Private Members ]
		
        private Vector3 relativeChange;
        private bool[ ] buttonPresses;
        private readonly Subject<Vector3> mouseMovements = new Subject<Vector3>( );
        private readonly Subject<MouseButton> mouseDowns = new Subject<MouseButton>( );
        private readonly Subject<MouseButton> mouseUps = new Subject<MouseButton>( );
        
		#endregion

        #region [ Public Properties ]

        /// <summary>
        /// Button presses 
        /// </summary>
        public bool[ ] ButtonPresses
        {
            get { return this.buttonPresses; }
            set
            {
                var oldButtons = this.buttonPresses ?? new bool[value.Length];
                this.buttonPresses = value;

                for ( int i = 0; i < value.Length; ++i )
                {
                    if ( oldButtons[ i ] != value[ i ] )
                        this.InvokeMouseButtonChanged( value[ i ], i );
                }

            }
        }

        /// <summary>
        /// Relative change in the mouse position
        /// </summary>
        public Vector3 RelativeChange
        {
            get { return this.relativeChange; }
            protected set
            {
                this.relativeChange = value;

                if ( value.X != 0 || value.Y != 0 )
                    this.mouseMovements.OnNext( value );
            }
        }

        public IObservable<MouseButton> MouseButtonDown
        {
            get { return this.mouseDowns.AsObservable( ); }
        }

        public IObservable<MouseButton> MouseButtonUp
        {
            get { return this.mouseUps.AsObservable( ); }
        }

        public IObservable<Vector3> MouseMove
        {
            get { return this.mouseMovements.AsObservable( ); }
        }
        
        #endregion

        protected Mouse( )
        {
        }

        protected abstract bool Capture( );

        #region [ Implementation of IService ]

        public abstract void Start( Kernel kernel );

        public abstract void Stop( Kernel kernel );

        public uint Priority
        {
            get { return 100; }
        }

        public void Update( GameTime gameTime )
        {
            this.Capture( );
        }

		public abstract string Name { get; }

		public ServiceStatus Status { get; protected set; }
		
        #endregion [ Implementation of IService ]
        
        #region [ Implementation of IDisposable ]

        ~Mouse( )
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

        #region [ Private Methods ]

        private void InvokeMouseButtonChanged( bool buttonDown, int button )
        {
            var handler = buttonDown ? this.mouseDowns : this.mouseUps;

            handler.OnNext( ( MouseButton )button );
        }

        #endregion [ Private Methods ]
    }
}
