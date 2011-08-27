using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Life
{
    public class Kernel : IEnumerable<IService>
    {
        #region [ Private Members ]

        private readonly LinkedList<IService> services = new LinkedList<IService>( );

        #endregion [ Private Members ]

        #region [ Implementation of IEnumerable ]

        public IEnumerator<IService> GetEnumerator( )
        {
            return this.services.GetEnumerator( );
        }

        IEnumerator IEnumerable.GetEnumerator( )
        {
            return this.GetEnumerator( );
        }

        #endregion [ Implementation of IEnumerable ]

        public bool AddService( IService service )
        {
            service.Start( this );

            lock ( this.services )
            {
                // Find the service with a higher priority number
                var current = this.services.First;

                // Loop until found
                while ( current != null && current.Value.Priority <= service.Priority )
                    current = current.Next;

                // None found or empty list
                if ( current == null )
                    this.services.AddLast( service );
                else
                    this.services.AddBefore( current, service );

                return true;
            }
        }
        
		public void Run( )
		{
			var time = new GameTime( );
			
			while( services.Any( ) )
			{
				var current = this.services.First;
				
				while( current != null )
				{
					var next = current.Next;
					
					if ( current.Value.Status == ServiceStatus.Alive )
						current.Value.Update( time );
					
					if ( current.Value.Status == ServiceStatus.Dead )
						this.services.Remove( current );
						
					current = next;
				}
			}
			
		}
    }
}
