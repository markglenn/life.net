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
		public void Ctor_WithVector4_SetsNormalAndD( )
		{
			Assert.AreEqual(
				new Plane( new Vector4( 1, 2, 3, 4 ) ),
				new Plane( 1, 2, 3, 4 ) );
		}
		
        [Test]
        public void DistanceTo_ReturnsDistanceFromPoint( )
        {
            var plane = new Plane( new Vector3( 0, 0, 2 ).Unit( ), 1 );

            Assert.AreEqual( 4, plane.DistanceTo( new Vector3( 1, 2, 3 ) ) );
        }

        [Test]
        public void Side_ReturnsPlanarIfPointOnPlane( )
        {
            var plane = new Plane( 0, 0, 1, 0 );

            Assert.AreEqual( PlaneSide.Coplanar, plane.Side( new Vector3( 0, 0, 0 ) ) );
        }

        [Test]
        public void Side_ReturnsFrontIfPointInFrontOfPlane( )
        {
            var plane = new Plane( 0, 0, 1, 0 );

            Assert.AreEqual( PlaneSide.Front, plane.Side( new Vector3( 0, 0, 1 ) ) );
        }

        [Test]
        public void Side_ReturnsBackIfPointInBackOfPlane( )
        {
            var plane = new Plane( 0, 0, 1, 0 );

            Assert.AreEqual( PlaneSide.Back, plane.Side( new Vector3( 0, 0, -1 ) ) );
        }
    }
}
