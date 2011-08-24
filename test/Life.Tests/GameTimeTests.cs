using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Life.Tests
{
    [TestFixture]
    public class GameTimeTests
    {
        [Test]
        public void Ctor_InitializesGameTime( )
        {
            var time = new GameTime( );

            Assert.AreEqual( TimeSpan.Zero, time.CurrentStep );
            Assert.AreEqual( TimeSpan.Zero, time.TotalTime );
        }

        [Test]
        public void Pause_PausesTimer( )
        {
            var time = new GameTime( );
            Assert.IsFalse( time.IsPaused );

            time.Pause( );

            Assert.IsTrue( time.IsPaused );
        }

        [Test]
        public void Resume_ResumesTimer( )
        {
            var time = new GameTime( );

            time.Pause( );
            time.Resume( );

            Assert.IsFalse( time.IsPaused );
        }
    }
}
