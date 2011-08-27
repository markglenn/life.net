using System;

namespace Life.Input
{
    public abstract class InputManager : IDisposable
    {
        public abstract Keyboard GetKeyboard( );

        public abstract Mouse GetMouse( );

        ~InputManager( )
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
    }
}

