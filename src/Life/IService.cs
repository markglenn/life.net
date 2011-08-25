using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Life
{
    public enum ServiceStatus
    {
        Alive,
        Paused,
        Dead
    }

    public interface IService : IDisposable
    {
        /// <summary>
        /// Friendly service name
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Current status of the service
        /// </summary>
        ServiceStatus Status { get; }

        /// <summary>
        /// Runs the startup of the service
        /// </summary>
        void Start( Kernel kernel );

        /// <summary>
        /// Stops a service
        /// </summary>
        void Stop( Kernel kernel );

        /// <summary>
        /// Priority of the service
        /// </summary>
        /// <remarks>Lower value priorities are earlier in the service queue</remarks>
        uint Priority { get; }

        /// <summary>
        /// Updates the service every frame
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        void Update( GameTime gameTime );
    }
}
