using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Life.Core.Tests
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
    }
}
