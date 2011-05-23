using System;
using NUnit.Framework;

namespace Life.Math.Tests
{
    [TestFixture]
    public class PlaneTests
    {
        [Test]
        public void Ctor_SetsNormalAndD( )
        {
            var plane = new Plane( new Vector3( 1, 2, 3 ), 4 );

            Assert.AreEqual( new Vector3( 1, 2, 3 ), plane.Normal );
            Assert.AreEqual( 4, plane.D );
        }
        
        [Test]
        public void Ctor_WithValues_SetsNormalAndD( )
        {
            var plane = new Plane( 1, 2, 3, 4 );

            Assert.AreEqual( new Vector3( 1, 2, 3 ), plane.Normal );
            Assert.AreEqual( 4, plane.D );
        }

        [Test]
        public void DistanceTo_ReturnsDistanceFromPoint( )
        {
            var plane = new Plane( new Vector3( 0, 0, 2 ).Unit( ), 1 );

            Assert.AreEqual( 4, plane.DistanceTo( new Vector3( 1, 2, 3 ) ) );
        }
    }
}
