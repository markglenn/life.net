using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Life.Core
{
    /// <summary>
    /// Contains a snapshot of a time to keep the game in sync
    /// </summary>
    public class GameTime
    {
        #region [ Private Members ]

        private readonly Stopwatch stopwatch = new Stopwatch( );
        private readonly Subject<GameTime> tick = new Subject<GameTime>( );

        #endregion [ Private Members ]

        #region [ Public Properties ]

        /// <summary>
        /// Is the timer paused
        /// </summary>
        public bool IsPaused { get; private set; }

        /// <summary>
        /// Total time that has elapsed since the start of the timer
        /// </summary>
        public TimeSpan TotalTime { get; private set; }

        /// <summary>
        /// Current step in time
        /// </summary>
        public TimeSpan CurrentStep { get; private set; }

        /// <summary>
        /// Occurs every tick of the clock
        /// </summary>
        public IObservable<GameTime> Tick
        {
            get { return this.tick.AsObservable( ); }
        }

        #endregion [ Public Properties ]

        public GameTime( )
        {
            this.stopwatch = Stopwatch.StartNew( );

            this.TotalTime = TimeSpan.Zero;
            this.CurrentStep = TimeSpan.Zero;
        }

        /// <summary>
        /// Pauses the game timer
        /// </summary>
        public void Pause( )
        {
            this.IsPaused = true;
            this.stopwatch.Stop( );
        }

        /// <summary>
        /// Resumes the game timer
        /// </summary>
        public void Resume( )
        {
            this.IsPaused = false;
            this.stopwatch.Start( );
        }

        /// <summary>
        /// Update the time to latest tick
        /// </summary>
        internal void Update( )
        {
            TimeSpan lastTime = this.TotalTime;

            this.TotalTime = TimeSpan.FromMilliseconds( this.stopwatch.ElapsedMilliseconds );
            this.CurrentStep = this.TotalTime - lastTime;
            this.tick.OnNext( this );
        }

    }
}
