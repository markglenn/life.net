using System;
using NUnit.Framework;

namespace Life.Math.Tests
{
    [TestFixture]
    public class RayTests
    {
        [Test]
        public void Ctor_SetsOriginAndDirection( )
        {
            var origin = new Vector3( 1, 2, 3 );
            var direction = new Vector3( 3, 2, 1 );

            var ray = new Ray( origin, direction );

            Assert.AreEqual( origin, ray.Origin );
            Assert.AreEqual( direction, ray.Direction );

        }
    }
}
