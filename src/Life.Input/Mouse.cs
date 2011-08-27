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
        private bool[ ] buttonPresses = new bool[ 3 ];
        private readonly Subject<Vector3> mouseMovements = new Subject<Vector3>( );
        private readonly Subject<MouseButton> mouseDowns = new Subject<MouseButton>( );
        private readonly Subject<MouseButton> mouseUps = new Subject<MouseButton>( );
        
		#endregion

        #region [ Public Properties ]

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

        #region [ Implementation of IService ]

        public virtual void Start( Kernel kernel )
		{
			this.Status = Life.ServiceStatus.Alive;
		}

        public virtual void Stop( Kernel kernel )
		{
			this.Status = Life.ServiceStatus.Dead;
		}

        public uint Priority
        {
            get { return 100; }
        }

        public abstract void Update( GameTime gameTime );

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

        protected virtual void Dispose( bool disposing )
		{
		}

        #endregion [ Implementation of IDisposable ]
        
		public void SetMouseButtonState( MouseButton button, bool isPressed )
		{
			if ( this.buttonPresses[ ( int )button ] != isPressed )
			{
				this.buttonPresses[ ( int )button ] = isPressed;
				this.InvokeMouseButtonChanged( isPressed, ( int )button );
			}
		}

        #region [ Private Methods ]

        private void InvokeMouseButtonChanged( bool buttonDown, int button )
        {
            var handler = buttonDown ? this.mouseDowns : this.mouseUps;

            handler.OnNext( ( MouseButton )button );
        }

        #endregion [ Private Methods ]
    }
}
